using System.Web.Mvc;
using HotelOrTaxi.Controllers;
using HotelOrTaxi.Models;
using NUnit.Framework;

namespace HotelOrTaxi.Tests.Controllers
{
    [TestFixture]
    public class TestResultsController
    {
        [Test]
        public void DisplaysIndex()
        {
            var resultsController = new ResultsController();
            ViewResult viewResult = resultsController.Index(null, null, null, null);

            string viewName = viewResult.ViewName;

            Assert.That(viewName, Is.EqualTo("Index"));
        }

        [Test]
        public void ReturnsViewModel()
        {
            var resultsController = new ResultsController();
            ViewResult viewResult = resultsController.Index(null, null, null, null);

            object model = viewResult.Model;

            Assert.That(model, Is.TypeOf<ResultsViewModel>());
        }

        [Test]
        public void ReturnsWinner()
        {
            var resultsController = new ResultsController();
            ViewResult viewResult = resultsController.Index(null, null, null, null);

            var model = (ResultsViewModel) viewResult.Model;

            Assert.That(model.Winner.Name, Is.EqualTo("Taxi"));
            Assert.That(model.Winner.Price, Is.EqualTo("£26"));
            Assert.That(model.Winner.Uri, Is.EqualTo("@Html.ActionLink(\"Show me\", \"Index\", \"Taxi\")"));
        }

        [Test]
        public void ReturnsLoser()
        {
            var resultsController = new ResultsController();
            ViewResult viewResult = resultsController.Index(null, null, null, null);

            var model = (ResultsViewModel) viewResult.Model;

            Assert.That(model.Loser.Name, Is.EqualTo("Hotel"));
            Assert.That(model.Loser.Price, Is.EqualTo("£30"));
            Assert.That(model.Loser.Uri,
                        Is.EqualTo(
                            "<a href =\"http://m.laterooms.com/en/p9827/MobileSearch.aspx?k=Manchester&d=20130404&n=1&adults=1&children=0&minp=&maxp=&StarRatingFilter=&rt=1-0&Latitude=&Longitude=&MaxRadius=1&PageSize=10&toStep=&SortBy=Price&Ascending=True&findButton=FIND+HOTELS&sb=tp&sd=true\">Show me</a></h3>"));
        }
    }
}