using System;
using System.Web.Mvc;
using Geography;
using HotelOrTaxi.Models;
using JourneyCalculator;
using Results;
using WebResponse;

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
                                       ICanGetTheDistanceOfATaxiJourneyBetweenPoints distanceCalculator,
                                       ICalculateTheWinner whoIsTheWinner)
        {
            _taxiResultFactory = taxiResultFactory;
            _hotelResultFactory = hotelResultFactory;
            _distanceCalculator = distanceCalculator;
            _whoIsTheWinner = whoIsTheWinner;
        }

        public ResultsViewModel Create(UrlHelper urlHelper, StartingPoint startingPoint, Destination destination)
        {
            Metres distance;
            HotelResult hotel = _hotelResultFactory.Create(startingPoint);

            try
            {
                distance = _distanceCalculator.Calculate(startingPoint, destination);
                var journey = new Journey(startingPoint, distance);
                TaxiResult taxi = _taxiResultFactory.Create(urlHelper, journey);
                return _whoIsTheWinner.Fight(taxi, hotel);
            }
            catch (Exception ex)
            {
                Type exceptionType = ex.GetType();
                if (exceptionType == typeof (TaxiApiException) || exceptionType == typeof (NoRouteFoundException))
                    return new ResultsViewModel
                        {
                            Winner = hotel,
                            Loser = null,
                            PriceDifference = 0.0
                        };
                throw;
            }
        }
    }
}