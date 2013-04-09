using System;

namespace TaxiFirmDetails
{
    public interface IConstructGooglePlaceRequests
    {
        string GetPlaceRequest(string placeReference);
    }

    public class GooglePlaceRequestConstructor : IConstructGooglePlaceRequests
    {
        private readonly ICanReadConfigurations _configReader;

        public GooglePlaceRequestConstructor(ICanReadConfigurations configReader)
        {
            _configReader = configReader;
        }

        public string GetPlaceRequest(string placeReference)
        {
            string baseUri = "https://maps.googleapis.com/maps/api/place/details/json";
            string sensor = "sensor=true";
            string key = "key=" + _configReader.ApiKey();
            string reference = "reference=" + placeReference;
            return String.Format("{0}?{1}&{2}&{3}", baseUri, reference, sensor, key);
        }
    }
}