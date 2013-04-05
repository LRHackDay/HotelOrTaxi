using System.Web.Mvc;
using Geography;
using HotelOrTaxi.Controllers;
using HotelOrTaxi.Models;
using JourneyCalculator;
using NUnit.Framework;

namespace HotelOrTaxi.Tests.Controllers
{
    [TestFixture]
    public class TestResultsController : ICreateResultViewModels, ICanGetTheDistanceOfATaxiJourneyBetweenPoints
    {
        [Test]
        public void DisplaysIndex()
        {
            ICreateResultViewModels resultsViewModelFactory = this;
            ICanGetTheDistanceOfATaxiJourneyBetweenPoints distanceCalculator = this;
            var resultsController = new ResultsController(resultsViewModelFactory, distanceCalculator);
            ViewResult viewResult = resultsController.Index(null, null, null, null);

            string viewName = viewResult.ViewName;

            Assert.That(viewName, Is.EqualTo("Index"));
        }

        [Test]
        public void ReturnsViewModel()
        {
            ICreateResultViewModels resultsViewModelFactory = this;
            ICanGetTheDistanceOfATaxiJourneyBetweenPoints distanceCalculator = this;
            var resultsController = new ResultsController(resultsViewModelFactory, distanceCalculator);
            ViewResult viewResult = resultsController.Index(null, null, null, null);

            object model = viewResult.Model;

            Assert.That(model, Is.TypeOf<ResultsViewModel>());
        }

        ResultsViewModel ICreateResultViewModels.Create(UrlHelper urlHelper, StartingPoint startingPoint, Destination destination)
        {
            return new ResultsViewModel();
        }

        Metres ICanGetTheDistanceOfATaxiJourneyBetweenPoints.Calculate(StartingPoint origin, Destination destination)
        {
            return new Metres(10);
        }
    }
}