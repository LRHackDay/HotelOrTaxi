using System;
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
                                       ICanGetTheDistanceOfATaxiJourneyBetweenPoints distanceCalculator,
                                       ICalculateTheWinner whoIsTheWinner)
        {
            _taxiResultFactory = taxiResultFactory;
            _hotelResultFactory = hotelResultFactory;
            _distanceCalculator = distanceCalculator;
            _whoIsTheWinner = whoIsTheWinner;
        }

        public ResultsViewModelFactory()
        {
            _hotelResultFactory = new HotelResultFactory();
            _taxiResultFactory = new TaxiResultFactory();
            _distanceCalculator = new DistanceCalculator();
            _whoIsTheWinner = new WhoIsTheWinner();
        }

        public ResultsViewModel Create(UrlHelper urlHelper, StartingPoint startingPoint, Destination destination)
        {
            TaxiResult taxi = null;
            HotelResult hotel = _hotelResultFactory.Create(startingPoint);

            Metres distance = _distanceCalculator.Calculate(startingPoint, destination);

            Journey journey = Journey(startingPoint, distance);

            if (distance != null && journey != null)
                taxi = _taxiResultFactory.Create(urlHelper, journey);

            if (taxi == null && hotel == null)
                throw new NoClearWinnerExeption();

            return _whoIsTheWinner.Fight(taxi, hotel);
        }

        private static Journey Journey(StartingPoint startingPoint, Metres distance)
        {
            try
            {
                return new Journey(startingPoint, distance);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}