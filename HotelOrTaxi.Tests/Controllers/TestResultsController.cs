using System.Web.Mvc;
using Geography;
using HotelOrTaxi.Controllers;
using HotelOrTaxi.Models;
using LateRoomsScraper;
using NUnit.Framework;
using Results;

namespace HotelOrTaxi.Tests.Controllers
{
    [TestFixture]
    public class TestResultsController : ICreateResultViewModels
    {
        [Test]
        public void DisplaysIndex()
        {
            ICreateResultViewModels resultsViewModelFactory = this;
            var resultsController = new ResultsController(resultsViewModelFactory);
            ViewResult viewResult = resultsController.Index(null, null, null, null);

            string viewName = viewResult.ViewName;

            Assert.That(viewName, Is.EqualTo("Index"));
        }

        [Test]
        public void ReturnsViewModel()
        {
            ICreateResultViewModels resultsViewModelFactory = this;
            var resultsController = new ResultsController(resultsViewModelFactory);
            ViewResult viewResult = resultsController.Index(null, null, null, null);

            object model = viewResult.Model;

            Assert.That(model, Is.TypeOf<ResultsViewModel>());
        }

        ResultsViewModel ICreateResultViewModels.Create(UrlHelper urlHelper, Journey journey)
        {
            return new ResultsViewModel();
        }
    }
}