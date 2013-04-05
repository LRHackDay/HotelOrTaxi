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
        private bool _throwError;

        [Test]
        public void ReturnsViewModel()
        {
            _throwError = false;
            _viewModel = new ResultsViewModel();

            var resultsViewModelFactory = new ResultsViewModelFactory(this, this, this, this);
            var startingPoint = new StartingPoint(null, null);
            ResultsViewModel resultsViewModel = resultsViewModelFactory.Create(null, startingPoint, null);

            Assert.That(resultsViewModel, Is.EqualTo(_viewModel));
        }

        [Test]
        public void OnlyReturnWinningHotelWhenNoTaxiRouteFound()
        {
            _throwError = true;

            _hotel = new HotelResult();
            _taxi = new TaxiResult();

            var resultsViewModelFactory = new ResultsViewModelFactory(this, this, this, this);
            var startingPoint = new StartingPoint(null, null);
            ResultsViewModel resultsViewModel = resultsViewModelFactory.Create(null, startingPoint, null);

            Assert.That(resultsViewModel.Loser, Is.Null);
            Assert.That(resultsViewModel.Winner, Is.EqualTo(_hotel));
        }

        TaxiResult ICreateTheTaxiResult.Create(UrlHelper urlHelper, Journey journey)
        {
            return _taxi;
        }

        HotelResult ICreateTheHotelResult.Create(StartingPoint startingPoint)
        {
            return _hotel;
        }

        Metres ICanGetTheDistanceOfATaxiJourneyBetweenPoints.Calculate(StartingPoint origin, Destination destination)
        {
            if (_throwError)
                throw new NoRouteFoundException();
            return new Metres(10);
        }

        ResultsViewModel ICalculateTheWinner.Fight(TaxiResult taxi, HotelResult hotel)
        {
            return _viewModel;
        }
    }
}