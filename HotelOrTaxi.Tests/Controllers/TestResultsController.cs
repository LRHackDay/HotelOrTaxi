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
    }
}