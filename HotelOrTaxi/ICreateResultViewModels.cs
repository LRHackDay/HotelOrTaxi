using System.Web.Mvc;
using HotelOrTaxi.Models;
using JourneyCalculator;
using Results;

namespace HotelOrTaxi
{
    public interface ICreateResultViewModels
    {
        ResultsViewModel Create(UrlHelper urlHelper, Journey journey);
    }

    public class ResultsViewModelFactory : ICreateResultViewModels
    {
        private readonly ICreateTheTaxiResult _taxiResultFactory;
        private readonly ICreateTheHotelResult _hotelResultFactory;

        public ResultsViewModelFactory(ICreateTheTaxiResult taxiResultFactory, ICreateTheHotelResult hotelResultFactory)
        {
            _taxiResultFactory = taxiResultFactory;
            _hotelResultFactory = hotelResultFactory;
        }

        public ResultsViewModel Create(UrlHelper urlHelper, Journey journey)
        {
            TaxiResult taxi = _taxiResultFactory.Create(urlHelper, journey);
            Result hotel = _hotelResultFactory.Create(journey);

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