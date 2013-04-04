using System.Web.Mvc;
using HotelOrTaxi.Models;
using Results;

namespace HotelOrTaxi
{
    public interface ICreateResultViewModels
    {
        ResultsViewModel Create(UrlHelper urlHelper);
    }

    public class ResultsViewModelFactory : ICreateResultViewModels
    {
        private readonly ICreateTheTaxiResult _taxiResultFactory;
        private readonly ICreateTheHotelResult _hotelResultFactory;

        public ResultsViewModelFactory(TaxiResultFactory taxiResultFactory, HotelResultFactory hotelResultFactory)
        {
            _taxiResultFactory = taxiResultFactory;
            _hotelResultFactory = hotelResultFactory;
        }

        public ResultsViewModel Create(UrlHelper urlHelper)
        {
            Result taxi = _taxiResultFactory.Create(urlHelper);
            Result hotel = _hotelResultFactory.Create();

            var resultsViewModel = new ResultsViewModel();
            resultsViewModel.Loser = hotel;
            resultsViewModel.Winner = taxi;
            return resultsViewModel;
        }
    }
}