using System;
using Geography;
using NUnit.Framework;
using TaxiApi.Configuration;
using TaxiApi.Request;
using TaxiApi.Response;

namespace TaxiApi.Tests
{
    [TestFixture]
    public class FareResponseFactoryTests : IPerformApiRequest, IConfiguration, ICalculateTheJourneyDistance
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
            var webClientApiRequest = new WebClientApiRequest(this);

            var latitude = new Latitude("52.51211199999999");
            var longitude = new Longitude("-3.3131060000000616");
            var startingPoint = new Location(latitude, longitude);
            var from = new StartingPoint(startingPoint);
            var destination = new Location(null, null);
            var to = new Destination(destination);

            _distance = new Metres(5000);

            var distanceFactory = this;
            var journey = new Journey(@from, to, distanceFactory);

            string request = new FareRequestFactory(this).Create(DateTime.Now, journey);

            FareResponse fareResponse = new FareResponseFactory(webClientApiRequest).Create(request);

            Assert.That(fareResponse.Fare.Cost, Is.EqualTo((decimal) 7.20));
        }

        public string Perform(string request)
        {
            return _response;
        }

        public string ApiUrl()
        {
            return "http://yourtaximeter.com/api/";
        }

        public string ApiKey()
        {
            return "test";
        }

        Metres ICalculateTheJourneyDistance.Create(StartingPoint @from, Destination to)
        {
            return _distance;
        }
    }
}