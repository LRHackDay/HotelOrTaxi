using HotelOrTaxi.Controllers;
using NUnit.Framework;

namespace HotelOrTaxi.Tests.Controllers
{
    [TestFixture]
    public class TestHomeController
    {
        [Test]
        public void DisplaysHomePage()
        {
            var viewResult = new HomeController().Index();

            var viewName = viewResult.ViewName;
            Assert.That(viewName, Is.EqualTo("Index"));
        }
    }

    [TestFixture]
    public class TestResultsController
    {
        [Test]
        public void DisplaysHomePage()
        {
            var resultsController = new ResultsController();
            var viewResult = resultsController.Index(null, null, null, null);

            var viewName = viewResult.ViewName;

            Assert.That(viewName, Is.EqualTo("Index"));
        }
    }
}