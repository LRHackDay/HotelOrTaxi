using System.Web.Mvc;
using HotelOrTaxi.Controllers;
using HotelOrTaxi.Models;

namespace HotelOrTaxi
{
    public interface ICreateResultViewModels
    {
        ResultsViewModel Create(UrlHelper urlHelper);
    }

    public class ResultsViewModelFactory : ICreateResultViewModels
    {
        private readonly ICreateTaxis _taxiFactory;
        private readonly ICreateHotels _createHotels;

        public ResultsViewModelFactory(TaxiFactory taxiFactory, CreateHotels createHotels)
        {
            _taxiFactory = taxiFactory;
            _createHotels = createHotels;
        }

        public ResultsViewModel Create(UrlHelper urlHelper)
        {
            var taxi = _taxiFactory.Create(urlHelper);
            var hotel = _createHotels.Create();

            var resultsViewModel = new ResultsViewModel();
            resultsViewModel.Loser = hotel;
            resultsViewModel.Winner = taxi;
            return resultsViewModel;
        }
    }

    public interface ICreateHotels
    {
        Result Create();
    }

    public class CreateHotels : ICreateHotels
    {
        public Result Create()
        {
            var hotel = new Result();
            hotel.Name = "Hotel";
            hotel.Price = "£30";
            hotel.Uri =
                "http://m.laterooms.com/en/p9827/MobileSearch.aspx?k=Manchester&d=20130404&n=1&adults=1&children=0&minp=&maxp=&StarRatingFilter=&rt=1-0&Latitude=&Longitude=&MaxRadius=1&PageSize=10&toStep=&SortBy=Price&Ascending=True&findButton=FIND+HOTELS&sb=tp&sd=true";
            return hotel;
        }
    }

    public interface ICreateTaxis
    {
        Result Create(UrlHelper urlHelper);
    }

    public class TaxiFactory : ICreateTaxis
    {
        private readonly ICreateTheTaxiControllerUri _createTheTaxiControllerUri;

        public TaxiFactory(ICreateTheTaxiControllerUri createTheTaxiControllerUri)
        {
            _createTheTaxiControllerUri = createTheTaxiControllerUri;
        }

        public Result Create(UrlHelper urlHelper)
        {
            Result taxi = new Result();
            taxi.Name = "Taxi";
            taxi.Price = "£26";
            taxi.Uri = _createTheTaxiControllerUri.GetUriForTaxi(urlHelper);
            return taxi;
        }
    }
}