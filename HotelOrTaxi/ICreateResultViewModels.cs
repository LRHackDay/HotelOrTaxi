using System.Web.Mvc;
using Geography;
using HotelOrTaxi.Models;
using JourneyCalculator;
using Results;

namespace HotelOrTaxi
{
    public interface ICreateResultViewModels
    {
        ResultsViewModel Create(UrlHelper urlHelper, StartingPoint startingPoint, Destination destination);
    }

    public class ResultsViewModelFactory : ICreateResultViewModels
    {
        private readonly ICreateTheTaxiResult _taxiResultFactory;
        private readonly ICreateTheHotelResult _hotelResultFactory;
        private readonly ICanGetTheDistanceOfATaxiJourneyBetweenPoints _distanceCalculator;

        public ResultsViewModelFactory(ICreateTheTaxiResult taxiResultFactory, ICreateTheHotelResult hotelResultFactory,
                                       ICanGetTheDistanceOfATaxiJourneyBetweenPoints distanceCalculator)
        {
            _taxiResultFactory = taxiResultFactory;
            _hotelResultFactory = hotelResultFactory;
            _distanceCalculator = distanceCalculator;
        }

        public ResultsViewModel Create(UrlHelper urlHelper, StartingPoint startingPoint, Destination destination)
        {
            Metres distance;
            Result hotel = _hotelResultFactory.Create(startingPoint);

            try
            {
                distance = _distanceCalculator.Calculate(startingPoint, destination);
            }
            catch (NoRouteFoundException)
            {
                return new ResultsViewModel
                {
                    Winner = hotel,
                    Loser = null,
                    PriceDifference = 0.0
                };
            }
            var journey = new Journey(startingPoint, distance);
            TaxiResult taxi = _taxiResultFactory.Create(urlHelper, journey);
            return Winner(taxi, hotel);
        }

        private static ResultsViewModel Winner(Result taxi, Result hotel)
        {
            if (taxi.Price < hotel.Price)
            {
                return new ResultsViewModel
                    {
                        Loser = hotel,
                        Winner = taxi,
                        PriceDifference = hotel.Price - taxi.Price
                    };
            }
            return new ResultsViewModel
                {
                    Loser = taxi,
                    Winner = hotel,
                    PriceDifference = taxi.Price - hotel.Price
                };
        }
    }
}