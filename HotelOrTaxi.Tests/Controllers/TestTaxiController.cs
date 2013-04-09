using Geography;
using HotelOrTaxi.Controllers;
using HotelOrTaxi.Models;
using NUnit.Framework;

namespace HotelOrTaxi.Tests.Controllers
{
    [TestFixture]
    public class TestTaxiController : ICreateTaxiViewModels
    {
        [Test]
        public void DisplaysIndex()
        {
            var viewResult = new TaxiController(this).Index("53.479251", "-2.247926");

            var viewName = viewResult.ViewName;
            Assert.That(viewName, Is.EqualTo("Index"));
        }

        [Test]
        public void ReturnsViewModel()
        {
            var viewResult = new TaxiController(this).Index("53.479251", "-2.247926");

            var model = viewResult.Model;
            Assert.That(model, Is.TypeOf<TaxisViewModel>());
        }

        public TaxisViewModel Create(Location location)
        {
            return new TaxisViewModel();
        }
    }
}