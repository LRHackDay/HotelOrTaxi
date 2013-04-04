using HotelOrTaxi.Controllers;
using HotelOrTaxi.Models;
using NUnit.Framework;

namespace HotelOrTaxi.Tests.Controllers
{
    [TestFixture]
    public class TestTaxiController
    {
        [Test]
        public void DisplaysIndex()
        {
            var viewResult = new TaxiController().Index();

            var viewName = viewResult.ViewName;
            Assert.That(viewName, Is.EqualTo("Index"));
        }

        [Test]
        public void ReturnsViewModel()
        {
            var viewResult = new TaxiController().Index();

            var model = viewResult.Model;
            Assert.That(model, Is.TypeOf<TaxisViewModel>());
        }
    }
}