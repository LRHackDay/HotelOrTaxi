using System;
using Geography;

namespace JourneyCalculator
{
    public class DistanceCalculator : ICanGetTheDistanceOfATaxiJourneyBetweenPoints
    {
        private readonly IGetTheResponseFromGoogleMapsDirectionsApi _googleMapsDirectionsResponse;
        private readonly IDeserialiseGoogleMapsDirectionsResponses _googleMapsApiDeserialiser;
        private readonly ISpecifyConditionsOfNoTaxiRoutesFound _specifyConditionsOfNoTaxiRoutesFound;

        public DistanceCalculator(IGetTheResponseFromGoogleMapsDirectionsApi googleMapsDirectionsResponse,
                                  IDeserialiseGoogleMapsDirectionsResponses googleMapsApiDeserialiser, ISpecifyConditionsOfNoTaxiRoutesFound noTaxiRoutesSpecification)
        {
            _googleMapsDirectionsResponse = googleMapsDirectionsResponse;
            _googleMapsApiDeserialiser = googleMapsApiDeserialiser;
            _specifyConditionsOfNoTaxiRoutesFound = noTaxiRoutesSpecification;
        }

        public DistanceCalculator()
        {
            _googleMapsDirectionsResponse = new GoogleMapsDirectionsResponse();
            _googleMapsApiDeserialiser = new GoogleMapsApiDeserialiser();
            _specifyConditionsOfNoTaxiRoutesFound = new NoTaxiRoutesFoundSpecification();
        }

        public Metres Calculate(StartingPoint origin, Destination destination)
        {
            try
            {
                string response = _googleMapsDirectionsResponse.Generate(origin, destination);

                DirectionsResponse googleMapsDirections =
                    _googleMapsApiDeserialiser.DeserializeResponse(response);

                if (_specifyConditionsOfNoTaxiRoutesFound.IsSatisfiedBy(googleMapsDirections))
                    throw new NoRouteFoundException();
                string actualDistance = DistanceInMetres(googleMapsDirections);

                return new Metres(actualDistance);
            }
            catch (NoRouteFoundException)
            {
                return null;
            }
        }

        private static string DistanceInMetres(DirectionsResponse directionsResponse)
        {
            Routes firstRoute = directionsResponse.Routes[0];
            Legs firstLeg = firstRoute.Legs[0];
            Distance legDistance = firstLeg.Distance;
            string distanceInMetres = legDistance.Value;
            return distanceInMetres;
        }
    }
}