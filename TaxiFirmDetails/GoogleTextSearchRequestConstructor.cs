using System;
using Geography;
using WebResponse;

namespace TaxiFirmDetails
{
    public interface IConstructGoogleTextSearchRequests
    {
        string GetTextSearchRequests(Location location);
    }

    public class GoogleTextSearchRequestConstructor : IConstructGoogleTextSearchRequests
    {
        private readonly ICanReadConfigurations _configReader;
        private readonly IDownloadResponses _webClientWrapper;

        public GoogleTextSearchRequestConstructor(ICanReadConfigurations configReader, IDownloadResponses webClientWrapper)
        {
            _configReader = configReader;
            _webClientWrapper = webClientWrapper;
        }

        public GoogleTextSearchRequestConstructor()
        {
            _configReader = new ConfigReader();
            _webClientWrapper = new WebClientWrapper();
        }

        public string GetTextSearchRequests(Location geoLocation)
        {
            string baseUri = "https://maps.googleapis.com/maps/api/place/textsearch/json";
            string location = "location=" + geoLocation;
            string key = "key=" + _configReader.ApiKey();
            string radius = "radius=100";
            string query = "query=taxi";
            string sensor = "sensor=true";

            var address = String.Format("{0}?{1}&{2}&{3}&{4}&{5}", baseUri, location, radius, query, sensor, key);

            return _webClientWrapper.Get(address);

        }
    }
}