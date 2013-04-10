using System;
using Configuration;
using Geography;
using JourneyCalculator;
using NUnit.Framework;
using TaxiApi.Request;
using TaxiApi.Response;
using WebResponse;

namespace TaxiApi.Tests
{
    [TestFixture]
    public class FareResponseFactoryTests : IPerformApiRequest, ICanReadConfigurations, ICalculateTheJourneyDistance
    {
        private string _response;
        private Metres _distance;

        [SetUp]
        public void Setup()
        {
            _response = null;
        }

        [Test]
        public void deserializes_json_response_to_fare_response_object()
        {
            _response =
                "{ \"fare\": { \"cost\": \"7.20\", \"waiting\": \"0.20 every 60 seconds\",\"waitingEstimate\": \"0.00\", \"reason\": \"Standard, All day\", \"warning\": \"\", \"tariff\": { \"key\": \"79a4a1\", \"id\": \"1551\" } }, \"district\": { \"name\": \"Powys - Powys\", \"url\": \"http://yourtaximeter.com/main/council/powys--powys\", \"id\": \"819\", \"enc\": \"\", \"supported\": true }, \"map\": null, \"routeInfo\": null, \"callbackID\": \"171718\" }";

            FareResponse fareResponse = new FareResponseFactory(this).Create(null);

            Assert.That(fareResponse.Fare.Cost, Is.EqualTo((decimal) 7.20));
        }

        [Test]
        [Ignore]
        public void makes_a_request_to_real_service()
        {
            IDownloadResponses webResponseReader = new WebClientWrapper();
            var webClientApiRequest = new WebClientApiRequest(this, webResponseReader);

            var latitude = new Latitude("52.51211199999999");
            var longitude = new Longitude("-3.3131060000000616");
            var startingPoint = new Location(latitude, longitude);
            var from = new StartingPoint(startingPoint, null);
            string searchTerm = "my postcode";
            var to = new Destination(null, searchTerm);

            _distance = new Metres(5000);

            var journey = new Journey(@from, ((ICanGetTheDistanceOfATaxiJourneyBetweenPoints) null).Calculate(@from, to));

            string request = new FareRequestFactory(this).Create(DateTime.Now, journey);

            FareResponse fareResponse = new FareResponseFactory(webClientApiRequest).Create(request);

            Assert.That(fareResponse.Fare.Cost, Is.EqualTo((decimal) 7.20));
        }

        public string Perform(string request)
        {
            return _response;
        }

        public string TaxiApiUrl()
        {
            return "http://yourtaximeter.com/api/";
        }

        public string TaxiApiKey()
        {
            return "test";
        }

        public string GooglePlacesApiKey()
        {
            throw new NotImplementedException();
        }

        public Metres Create(StartingPoint @from, Destination to)
        {
            return _distance;
        }
    }
}