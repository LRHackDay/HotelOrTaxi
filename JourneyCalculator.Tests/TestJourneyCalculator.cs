using System.Collections.Generic;
using Geography;
using NUnit.Framework;

namespace JourneyCalculator.Tests
{
    [TestFixture]
    public class TestJourneyCalculator : ISpecifyConditionsOfNoTaxiRoutesFound,
                                         IGetTheResponseFromGoogleMapsDirectionsApi,
                                         IDeserialiseGoogleMapsDirectionsResponses
    {
        private bool _noRoutesFound;
        private string _distance;

        [Test]
        public void ReturnsNullWhenNoRouteFound()
        {
            _noRoutesFound = true;

            ISpecifyConditionsOfNoTaxiRoutesFound specifyConditionsOfNoTaxiRoutesFound = this;
            IGetTheResponseFromGoogleMapsDirectionsApi googleMapsDirectionsResponse = this;
            IDeserialiseGoogleMapsDirectionsResponses googleMapsApiDeserialiser = this;
            var directionsFactory =
                new DistanceCalculator(googleMapsDirectionsResponse, googleMapsApiDeserialiser,
                                       specifyConditionsOfNoTaxiRoutesFound);

            Assert.That(directionsFactory.Calculate(null, null), Is.EqualTo(null));
        }

        [Test]
        public void ReturnsTheTotalDistanceInMetresOfTheFirstRoute()
        {
            _noRoutesFound = false;
            _distance = "2137146";

            ISpecifyConditionsOfNoTaxiRoutesFound specifyConditionsOfNoTaxiRoutesFound = this;

            IGetTheResponseFromGoogleMapsDirectionsApi googleMapsDirectionsResponse = this;
            IDeserialiseGoogleMapsDirectionsResponses googleMapsApiDeserialiser = this;

            var directionsFactory = new DistanceCalculator(googleMapsDirectionsResponse, googleMapsApiDeserialiser,
                                                           specifyConditionsOfNoTaxiRoutesFound);
            Metres distance = directionsFactory.Calculate(null, null);
            Assert.That(distance.ToString(), Is.EqualTo(_distance));
        }

        bool ISpecifyConditionsOfNoTaxiRoutesFound.IsSatisfiedBy(DirectionsResponse googleMapsDirections)
        {
            return _noRoutesFound;
        }

        /// <summary>
        /// https://developers.google.com/maps/documentation/directions/#JSON
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        string IGetTheResponseFromGoogleMapsDirectionsApi.Generate(StartingPoint origin, Destination destination)
        {
            return "{\"routes\": [ {\"legs\": [ \"distance\": {\"value\": 2137146,\"text\": \"1,328 mi\"} ] ]}";
        }

        DirectionsResponse IDeserialiseGoogleMapsDirectionsResponses.DeserializeResponse(string response)
        {
            Routes routes = new Routes();
            routes.Legs = new List<Legs>
                {
                    new Legs
                        {
                            Distance = new Distance
                                {
                                    Value = _distance
                                }
                        }
                };
            var directionsResponse = new DirectionsResponse();
            List<Routes> listOfRoutes = new List<Routes>();
            listOfRoutes.Add(routes);
            directionsResponse.Routes = listOfRoutes;



            return directionsResponse;
        }
    }
}