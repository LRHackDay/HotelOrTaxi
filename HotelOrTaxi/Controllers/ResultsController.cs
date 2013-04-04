using System.Web.Mvc;
using HotelOrTaxi.Models;


namespace HotelOrTaxi.Controllers
{
    public class ResultsController : Controller
    {
        public ViewResult Index(string from, string to, string fromlatlong, string tolatlong)
        {
            var resultsViewModel = new ResultsViewModel();
            Result winner = new Result
                {
                    Name = "Taxi",
                    Price = "£26",
                    Uri = "@Html.ActionLink(\"Show me\", \"Index\", \"Taxi\")"
                };
            resultsViewModel.Winner = winner;
            Result loser = new Result
                {
                    Name = "Hotel",
                    Price = "£30",
                    Uri = "<a href =\"http://m.laterooms.com/en/p9827/MobileSearch.aspx?k=Manchester&d=20130404&n=1&adults=1&children=0&minp=&maxp=&StarRatingFilter=&rt=1-0&Latitude=&Longitude=&MaxRadius=1&PageSize=10&toStep=&SortBy=Price&Ascending=True&findButton=FIND+HOTELS&sb=tp&sd=true\">Show me</a></h3>"
                };
            resultsViewModel.Loser = loser;

            return View("Index", resultsViewModel);
        }
    }
}