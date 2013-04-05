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
            var viewResult = new TaxiController(this).Index();

            var viewName = viewResult.ViewName;
            Assert.That(viewName, Is.EqualTo("Index"));
        }

        [Test]
        public void ReturnsViewModel()
        {
            var viewResult = new TaxiController(this).Index();

            var model = viewResult.Model;
            Assert.That(model, Is.TypeOf<TaxisViewModel>());
        }

        public TaxisViewModel Create()
        {
            return new TaxisViewModel();
        }
    }
}