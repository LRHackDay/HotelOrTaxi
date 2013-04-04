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
        private readonly ICreateTheTaxiControllerUri _createTheTaxiControllerUri;

        public ResultsViewModelFactory(ICreateTheTaxiControllerUri createTheTaxiControllerUri)
        {
            _createTheTaxiControllerUri = createTheTaxiControllerUri;
        }

        public ResultsViewModel Create(UrlHelper urlHelper)
        {
            var winner = CreateWinner(urlHelper);
            var loser = Result();
            var resultsViewModel = new ResultsViewModel();
            resultsViewModel.Loser = loser;
            resultsViewModel.Winner = winner;
            return resultsViewModel;
        }

        private static Result Result()
        {
            Result loser = new Result();
            loser.Name = "Hotel";
            loser.Price = "£30";
            loser.Uri =
                "http://m.laterooms.com/en/p9827/MobileSearch.aspx?k=Manchester&d=20130404&n=1&adults=1&children=0&minp=&maxp=&StarRatingFilter=&rt=1-0&Latitude=&Longitude=&MaxRadius=1&PageSize=10&toStep=&SortBy=Price&Ascending=True&findButton=FIND+HOTELS&sb=tp&sd=true";
            return loser;
        }

        private Result CreateWinner(UrlHelper urlHelper)
        {
            Result winner = new Result();
            winner.Name = "Taxi";
            winner.Price = "£26";
            winner.Uri = _createTheTaxiControllerUri.GetUriForTaxi(urlHelper);
            return winner;
        }
    }
}