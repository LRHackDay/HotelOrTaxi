using Geography;
using NUnit.Framework;
using WebResponse;

namespace GoogleMapsApi.Tests
{
    [TestFixture]
    public class IntegrationTestGoogleMapsApi
    {
        [Test]
        public void ReturnsDistance()
        {
            var expectedDistance = new Metres(542383);
            ICanDownloadResponses webResponseDownloaderWrapper = new WebClientWrapper();
            Metres actualDistance = new DirectionsFactory(webResponseDownloaderWrapper).GetDistance();
            Assert.That(actualDistance, Is.EqualTo(expectedDistance));
        }
    }
}