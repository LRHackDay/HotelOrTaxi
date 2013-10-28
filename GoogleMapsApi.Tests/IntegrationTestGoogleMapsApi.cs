using Geography;
using JourneyCalculator;
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
            var expectedDistance = new Metres(542389);
            IDownloadResponses webResponseDownloaderWrapper = new WebClientWrapper();
            var specifyConditionsOfNoTaxiRoutesFound = new NoTaxiRoutesFoundSpecification();
            IGetTheResponseFromGoogleMapsDirectionsApi googleMapsDirectionsResponse = new GoogleMapsDirectionsResponse(webResponseDownloaderWrapper);
            var googleMapsApiDeserialiser = new GoogleMapsApiDeserialiser();

            var directionsFactory = new DistanceCalculator(googleMapsDirectionsResponse, googleMapsApiDeserialiser, specifyConditionsOfNoTaxiRoutesFound);

            var startingPoint = new StartingPoint(null, "Toronto");
            var destination = new Destination(null, "Montreal");

            Metres actualDistance = directionsFactory.Calculate(startingPoint, destination);
            Assert.That(actualDistance, Is.EqualTo(expectedDistance));
        }
    }
}
