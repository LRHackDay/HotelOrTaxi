using System.Web.Mvc;
using NUnit.Framework;
using Results;

namespace HotelOrTaxi.Tests
{
    [TestFixture]
    public class TestResultsViewModelFactory : ICreateTheTaxiResult, ICreateTheHotelResult
    {
        private Result _hotel;
        private Result _taxi;

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

            Result winner = new ResultsViewModelFactory(this, this).Create(null).Winner;
            
            Assert.That(winner, Is.EqualTo(_hotel));
        }

        Result ICreateTheTaxiResult.Create(UrlHelper urlHelper)
        {
            return _taxi;
        }

        Result ICreateTheHotelResult.Create()
        {
            return _hotel;
        }
    }
}