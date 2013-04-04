using System.Web.Mvc;
using HotelOrTaxi.Controllers;
using HotelOrTaxi.Models;
using NUnit.Framework;
using Results;

namespace HotelOrTaxi.Tests.Controllers
{
    [TestFixture]
    public class TestResultsController : ICreateTheTaxiControllerUri
    {
        private string _taxiUri;

        [Test]
        public void DisplaysIndex()
        {
            ICreateResultViewModels resultsViewModelFactory = new ResultsViewModelFactory(new TaxiResultFactory(this), new HotelResultFactory());
            var resultsController = new ResultsController(resultsViewModelFactory);
            ViewResult viewResult = resultsController.Index(null, null, null, null);

            string viewName = viewResult.ViewName;

            Assert.That(viewName, Is.EqualTo("Index"));
        }

        [Test]
        public void ReturnsViewModel()
        {
            ICreateResultViewModels resultsViewModelFactory = new ResultsViewModelFactory(new TaxiResultFactory(this), new HotelResultFactory());
            var resultsController = new ResultsController(resultsViewModelFactory);
            ViewResult viewResult = resultsController.Index(null, null, null, null);

            object model = viewResult.Model;

            Assert.That(model, Is.TypeOf<ResultsViewModel>());
        }

        [Test]
        public void ReturnsWinner()
        {
            _taxiUri = "bob";
            ICreateResultViewModels resultsViewModelFactory = new ResultsViewModelFactory(new TaxiResultFactory(this), new HotelResultFactory());
            var resultsController = new ResultsController(resultsViewModelFactory);
            ViewResult viewResult = resultsController.Index(null, null, null, null);

            var model = (ResultsViewModel) viewResult.Model;

            Assert.That(model.Winner.Name, Is.EqualTo("Taxi"));
            Assert.That(model.Winner.Price, Is.EqualTo("£26"));
            Assert.That(model.Winner.Uri, Is.EqualTo(_taxiUri));
        }

        [Test]
        public void ReturnsLoser()
        {
            ICreateResultViewModels resultsViewModelFactory = new ResultsViewModelFactory(new TaxiResultFactory(this), new HotelResultFactory());
            var resultsController = new ResultsController(resultsViewModelFactory);
            ViewResult viewResult = resultsController.Index(null, null, null, null);

            var model = (ResultsViewModel) viewResult.Model;

            Assert.That(model.Loser.Name, Is.EqualTo("Hotel"));
            Assert.That(model.Loser.Price, Is.EqualTo("£30"));
            Assert.That(model.Loser.Uri,
                        Is.EqualTo("http://m.laterooms.com/en/p9827/MobileSearch.aspx?k=Manchester&d=20130404&n=1&adults=1&children=0&minp=&maxp=&StarRatingFilter=&rt=1-0&Latitude=&Longitude=&MaxRadius=1&PageSize=10&toStep=&SortBy=Price&Ascending=True&findButton=FIND+HOTELS&sb=tp&sd=true"));
        }

        public string GetUriForTaxi(UrlHelper url)
        {
            return _taxiUri;
        }
    }
}