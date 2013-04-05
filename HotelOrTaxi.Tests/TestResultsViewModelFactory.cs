using System.Web.Mvc;
using Geography;
using JourneyCalculator;
using NUnit.Framework;
using Results;

namespace HotelOrTaxi.Tests
{
    [TestFixture]
    public class TestResultsViewModelFactory : ICreateTheTaxiResult, ICreateTheHotelResult, ICanGetTheDistanceOfATaxiJourneyBetweenPoints
    {
        private Result _hotel;
        private TaxiResult _taxi;
        private Metres _distance;

        [Test]
        public void SetsLowestPriceAsWinner()
        {
            _distance = new Metres(10);

            _hotel = new HotelResult
                {
                    Price = 10.00
                };

            _taxi = new TaxiResult
                {
                    Price = 20.00
                };

            Result winner = new ResultsViewModelFactory(this, this, this).Create(null, new StartingPoint(null, null), null).Winner;

            Assert.That(winner, Is.EqualTo(_hotel));
        }

        TaxiResult ICreateTheTaxiResult.Create(UrlHelper urlHelper, Journey journey)
        {
            return _taxi;
        }

        Result ICreateTheHotelResult.Create(StartingPoint startingPoint)
        {
            return _hotel;
        }

        public Metres Calculate(StartingPoint origin, Destination destination)
        {
            return _distance;
        }
    }
}