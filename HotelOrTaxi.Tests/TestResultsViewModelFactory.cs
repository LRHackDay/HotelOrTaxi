using System.Web.Mvc;
using Geography;
using HotelOrTaxi.Models;
using JourneyCalculator;
using NUnit.Framework;
using Results;

namespace HotelOrTaxi.Tests
{
    [TestFixture]
    public class TestResultsViewModelFactory : ICreateTheTaxiResult, ICreateTheHotelResult,
                                               ICanGetTheDistanceOfATaxiJourneyBetweenPoints, ICalculateTheWinner
    {
        private HotelResult _hotel;
        private TaxiResult _taxi;
        private ResultsViewModel _viewModel;
        private Metres _distance;

        [Test]
        public void ReturnsViewModel()
        {
            _viewModel = new ResultsViewModel();
            _distance = new Metres(0);
            _taxi = new TaxiResult();
            _hotel = new HotelResult();

            var resultsViewModelFactory = new ResultsViewModelFactory(this, this, this, this);
            var startingPoint = new StartingPoint(null, null);
            var resultsViewModel = resultsViewModelFactory.Create(null, startingPoint, null);

            Assert.That(resultsViewModel, Is.EqualTo(_viewModel));
        }

        [Test]
        public void OnlyReturnWinningHotelWhenNoTaxiRouteFound()
        {
            _hotel = new HotelResult();
            _taxi = new TaxiResult();
            _distance = null;

            var resultsViewModelFactory = new ResultsViewModelFactory(this, this, this, new WhoIsTheWinner());
            var startingPoint = new StartingPoint(null, null);
            var resultsViewModel = resultsViewModelFactory.Create(null, startingPoint, null);

            Assert.That(resultsViewModel.Loser, Is.Null);
            Assert.That(resultsViewModel.Winner, Is.EqualTo(_hotel));
        }

        [Test]
        public void OnlyReturnWinningHotelWhenNoTaxiResult()
        {
            _hotel = new HotelResult();
            _taxi = null;

            var resultsViewModelFactory = new ResultsViewModelFactory(this, this, this, new WhoIsTheWinner());
            var startingPoint = new StartingPoint(null, null);
            var resultsViewModel = resultsViewModelFactory.Create(null, startingPoint, null);

            Assert.That(resultsViewModel.Loser, Is.Null);
            Assert.That(resultsViewModel.Winner, Is.EqualTo(_hotel));
        }

        [Test]
        public void DealWithNoHotelFound()
        {
            _distance = new Metres(0);
            _hotel = null;
            _taxi = new TaxiResult();

            var resultsViewModelFactory = new ResultsViewModelFactory(this, this, this, new WhoIsTheWinner());
            var startingPoint = new StartingPoint(null, null);
            var resultsViewModel = resultsViewModelFactory.Create(null, startingPoint, null);

            Assert.That(resultsViewModel.Loser, Is.Null);
            Assert.That(resultsViewModel.Winner, Is.EqualTo(_taxi));
        }

        public TaxiResult Create(UrlHelper urlHelper, Journey journey)
        {
            return _taxi;
        }

        public HotelResult Create(StartingPoint startingPoint)
        {
            return _hotel;
        }

        public Metres Calculate(StartingPoint origin, Destination destination)
        {
            return _distance;
        }

        public ResultsViewModel Fight(TaxiResult taxi, HotelResult hotel)
        {
            return _viewModel;
        }
    }
}