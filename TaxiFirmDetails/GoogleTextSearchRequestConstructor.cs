using System;
using Geography;

namespace TaxiFirmDetails
{
    public interface IConstructGoogleTextSearchRequests
    {
        string GetTextSearchRequests(Location location);
    }

    public class GoogleTextSearchRequestConstructor : IConstructGoogleTextSearchRequests
    {
        private readonly ICanReadConfigurations _configReader;

        public GoogleTextSearchRequestConstructor(ICanReadConfigurations configReader)
        {
            _configReader = configReader;
        }

        public string GetTextSearchRequests(Location geoLocation)
        {
            string baseUri = "https://maps.googleapis.com/maps/api/place/textsearch/json";
            string location = "location=" + geoLocation;
            string key = "key=" + _configReader.ApiKey();
            string radius = "radius=100";
            string query = "query=taxi";
            string sensor = "sensor=true";

            return String.Format("{0}?{1}&{2}&{3}&{4}&{5}", baseUri, location, radius, query, sensor, key);
        }
    }
}