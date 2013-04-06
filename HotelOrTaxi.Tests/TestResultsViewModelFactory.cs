using System.Web.Mvc;
using Geography;
using HotelOrTaxi.Models;
using JourneyCalculator;
using NUnit.Framework;
using Results;
using WebResponse;

namespace HotelOrTaxi.Tests
{
    [TestFixture]
    public class TestResultsViewModelFactory : ICreateTheTaxiResult, ICreateTheHotelResult,
                                               ICanGetTheDistanceOfATaxiJourneyBetweenPoints, ICalculateTheWinner
    {
        private HotelResult _hotel;
        private TaxiResult _taxi;
        private ResultsViewModel _viewModel;
        private bool _throwNoTaxiRouteFoundException;
        private bool _throwNoHotelFoundException;
        private bool _throwTaxiApiException;
        private Metres _distance;

        [Test]
        public void ReturnsViewModel()
        {
            _viewModel = new ResultsViewModel();
            _distance = new Metres(0);
            _taxi = new TaxiResult();
            _hotel = new HotelResult();
            _throwNoHotelFoundException = false;
            _throwNoTaxiRouteFoundException = false;
            _throwTaxiApiException = false;

            var resultsViewModelFactory = new ResultsViewModelFactory(this, this, this, this);
            var startingPoint = new StartingPoint(null, null);
            var resultsViewModel = resultsViewModelFactory.Create(null, startingPoint, null);

            Assert.That(resultsViewModel, Is.EqualTo(_viewModel));
        }

        [Test]
        public void OnlyReturnWinningHotelWhenNoTaxiRouteFound()
        {
            _throwNoTaxiRouteFoundException = true;
            _throwNoHotelFoundException = false;
            _throwTaxiApiException = false;

            _hotel = new HotelResult();
            _taxi = new TaxiResult();

            var resultsViewModelFactory = new ResultsViewModelFactory(this, this, this, new WhoIsTheWinner());
            var startingPoint = new StartingPoint(null, null);
            var resultsViewModel = resultsViewModelFactory.Create(null, startingPoint, null);

            Assert.That(resultsViewModel.Loser, Is.Null);
            Assert.That(resultsViewModel.Winner, Is.EqualTo(_hotel));
        }

        [Test]
        public void OnlyReturnWinningHotelWhenProblemWithTaxiApi()
        {
            _throwTaxiApiException = true;
            _throwNoHotelFoundException = false;
            _throwNoTaxiRouteFoundException = false;

            _hotel = new HotelResult();
            _taxi = new TaxiResult();

            var resultsViewModelFactory = new ResultsViewModelFactory(this, this, this, new WhoIsTheWinner());
            var startingPoint = new StartingPoint(null, null);
            var resultsViewModel = resultsViewModelFactory.Create(null, startingPoint, null);

            Assert.That(resultsViewModel.Loser, Is.Null);
            Assert.That(resultsViewModel.Winner, Is.EqualTo(_hotel));
        }

        [Test]
        public void DealWithNoHotelFound()
        {
            _throwNoHotelFoundException = true;
            _throwTaxiApiException = false;
            _throwNoTaxiRouteFoundException = false;

            _distance = new Metres(0);
            _hotel = new HotelResult();
            _taxi = new TaxiResult();

            var resultsViewModelFactory = new ResultsViewModelFactory(this, this, this, new WhoIsTheWinner());
            var startingPoint = new StartingPoint(null, null);
            var resultsViewModel = resultsViewModelFactory.Create(null, startingPoint, null);

            Assert.That(resultsViewModel.Loser, Is.Null);
            Assert.That(resultsViewModel.Winner, Is.EqualTo(_taxi));
        }

        TaxiResult ICreateTheTaxiResult.Create(UrlHelper urlHelper, Journey journey)
        {
            if (_throwTaxiApiException)
                throw new TaxiApiException();
            return _taxi;
        }

        HotelResult ICreateTheHotelResult.Create(StartingPoint startingPoint)
        {
            if (_throwNoHotelFoundException)
                throw new NoHotelFoundException();
            return _hotel;
        }

        Metres ICanGetTheDistanceOfATaxiJourneyBetweenPoints.Calculate(StartingPoint origin, Destination destination)
        {
            if (_throwNoTaxiRouteFoundException)
                throw new NoRouteFoundException();
            return _distance;
        }

        public ResultsViewModel Fight(TaxiResult taxi, HotelResult hotel)
        {
            return _viewModel;
        }
    }
}