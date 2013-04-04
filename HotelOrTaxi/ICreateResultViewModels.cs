using System.Web.Mvc;
using Geography;
using HotelOrTaxi.Models;
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
            var taxi = _taxiResultFactory.Create(urlHelper);
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
                        Winner = taxi
                    };
            }
            return new ResultsViewModel
                {
                    Loser = taxi,
                    Winner = hotel
                };
        }
    }
}