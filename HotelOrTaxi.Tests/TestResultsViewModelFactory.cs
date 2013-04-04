using System.Web.Mvc;
using Geography;
using NUnit.Framework;
using Results;

namespace HotelOrTaxi.Tests
{
    [TestFixture]
    public class TestResultsViewModelFactory : ICreateTheTaxiResult, ICreateTheHotelResult
    {
        private Result _hotel;
        private TaxiResult _taxi;

        [Test]
        public void SetsLowestPriceAsWinner()
        {
            _hotel = new HotelResult
                {
                    Price = 10.00
                };

            _taxi = new TaxiResult
                {
                    Price = 20.00
                };

            Result winner = new ResultsViewModelFactory(this, this).Create(null, null).Winner;
            
            Assert.That(winner, Is.EqualTo(_hotel));
        }

        TaxiResult ICreateTheTaxiResult.Create(UrlHelper urlHelper)
        {
            return _taxi;
        }

        Result ICreateTheHotelResult.Create(Journey journey)
        {
            return _hotel;
        }
    }
}