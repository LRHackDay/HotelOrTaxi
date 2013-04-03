using System;
using NUnit.Framework;
using TaxiApi.Configuration;
using TaxiApi.Request;
using TaxiApi.Response;

namespace TaxiApi.Tests
{
    [TestFixture]
    public class FareResponseFactoryTests : IPerformApiRequest, IConfiguration
    {
        private string _response;

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

            var fareResponse = new FareResponseFactory(this).Create(null);

            Assert.That(fareResponse.Fare.Cost, Is.EqualTo((decimal)7.20));
        }

        [Test]
        [Ignore]
        public void makes_a_request_to_real_service()
        {
            var webClientApiRequest = new WebClientApiRequest(this);

            var latitude = new Latitude("52.51211199999999");
            var longitude = new Longitude("-3.3131060000000616");
            var journey = new Journey
                {
                    Distance = new Metres(5000),
                    Passengers = new Passengers(2),
                    StartingPoint = new Location(latitude, longitude)
                };
            var request = new FareRequestFactory(this).Create(DateTime.Now, journey);

            var fareResponse = new FareResponseFactory(webClientApiRequest).Create(request);

            Assert.That(fareResponse.Fare.Cost, Is.EqualTo((decimal)7.20));
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
    }
}