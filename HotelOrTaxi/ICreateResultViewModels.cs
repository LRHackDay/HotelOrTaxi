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
        private readonly ICalculateTheWinner _whoIsTheWinner;

        public ResultsViewModelFactory(ICreateTheTaxiResult taxiResultFactory, ICreateTheHotelResult hotelResultFactory,
                                       ICanGetTheDistanceOfATaxiJourneyBetweenPoints distanceCalculator, ICalculateTheWinner whoIsTheWinner)
        {
            _taxiResultFactory = taxiResultFactory;
            _hotelResultFactory = hotelResultFactory;
            _distanceCalculator = distanceCalculator;
            _whoIsTheWinner = whoIsTheWinner;
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
            return _whoIsTheWinner.Winner(taxi, hotel);
        }
    }
}