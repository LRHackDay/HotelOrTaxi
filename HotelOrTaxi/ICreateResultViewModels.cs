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
            Metres distance = null;
            TaxiResult taxi = null;
            HotelResult hotel = null;
            try
            {
                hotel = _hotelResultFactory.Create(startingPoint);
            }
            catch (NoHotelFoundException)
            {
            }

            try
            {
                distance = _distanceCalculator.Calculate(startingPoint, destination);
            }
            catch (NoRouteFoundException)
            {
            }

            Journey journey = null;
            try
            {
                journey = new Journey(startingPoint, distance);
            }
            catch (Exception)
            {
            }

            if (distance != null && journey != null)
            {
                
                try
                {
                    taxi = _taxiResultFactory.Create(urlHelper, journey);
                }
                catch (TaxiApiException)
                {
                }
            }

            if (taxi == null && hotel == null)
            {
                throw new NoClearWinnerExeption();
            }
            return _whoIsTheWinner.Fight(taxi, hotel);
        }
    }
}