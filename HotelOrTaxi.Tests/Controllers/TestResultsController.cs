using System;
using System.Web.Mvc;
using Geography;
using HotelOrTaxi.Controllers;
using HotelOrTaxi.Models;
using JourneyCalculator;
using NUnit.Framework;

namespace HotelOrTaxi.Tests.Controllers
{
    [TestFixture]
    public class TestResultsController : ICreateResultViewModels, ICreateLocations
    {
        private bool _throwError;

        [Test]
        public void DisplaysIndex()
        {
            ICreateResultViewModels resultsViewModelFactory = this;
            ICreateLocations locationFactory = this;
            var resultsController = new ResultsController(resultsViewModelFactory, locationFactory);
            ViewResult viewResult = resultsController.Index(null, null, null, null);

            string viewName = viewResult.ViewName;

            Assert.That(viewName, Is.EqualTo("Index"));
        }

        [Test]
        public void ReturnsViewModel()
        {

            ICreateResultViewModels resultsViewModelFactory = this;
            var resultsController = new ResultsController(resultsViewModelFactory, new LocationFactory());
            ViewResult viewResult = resultsController.Index(null, null, null, null);

            object model = viewResult.Model;

            Assert.That(model, Is.TypeOf<ResultsViewModel>());
        }

        [Test]
        public void OnErrorShowsErrorView()
        {
            _throwError = true;

            ICreateResultViewModels resultsViewModelFactory = this;
            var resultsController = new ResultsController(resultsViewModelFactory, new LocationFactory());
            ViewResult viewResult = resultsController.Index(null, null, null, null);

            string viewName = viewResult.ViewName;

            Assert.That(viewName, Is.EqualTo("Error"));
        }

        public ResultsViewModel Create(UrlHelper urlHelper, StartingPoint startingPoint, Destination destination)
        {
            if (_throwError)
                throw new Exception();
            return new ResultsViewModel();
        }

        public Location GetLocation(string latlong)
        {
            return new Location("bob");
        }
    }
}