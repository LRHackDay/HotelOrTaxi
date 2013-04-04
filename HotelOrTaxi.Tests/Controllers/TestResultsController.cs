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
            var viewResult = resultsController.Index(null, null, null, null);

            var viewName = viewResult.ViewName;

            Assert.That(viewName, Is.EqualTo("Index"));
        }

        [Test]
        public void ReturnsViewModel()
        {
            var resultsController = new ResultsController();
            var viewResult = resultsController.Index(null, null, null, null);

            var model = viewResult.Model;

            Assert.That(model, Is.TypeOf<ResultsViewModel>());
        }

        [Test]
        public void ReturnsWinner()
        {
            var resultsController = new ResultsController();
            var viewResult = resultsController.Index(null, null, null, null);

            var model = (ResultsViewModel)viewResult.Model;

            Assert.That(model.Winner.Name, Is.EqualTo("Taxi"));
            Assert.That(model.Winner.Price, Is.EqualTo("£26"));
            Assert.That(model.Winner.Uri, Is.EqualTo("@Html.ActionLink(\"Show me\", \"Index\", \"Taxi\")"));
        }

    }
}