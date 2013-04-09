using System;
using System.Collections.Generic;
using Geography;
using WebResponse;

namespace JourneyCalculator
{
    public interface IGetTheResponseFromGoogleMapsDirectionsApi
    {
        string Generate(StartingPoint origin, Destination destination);
    }

    /// <summary>
    /// https://developers.google.com/maps/documentation/directions/#DirectionsResponseElements
    /// </summary>
    public class DirectionsResponse
    {
        public List<Routes> Routes { get; set; }
    }

    public class GoogleMapsDirectionsResponse : IGetTheResponseFromGoogleMapsDirectionsApi
    {
        private readonly IDownloadResponses _webResponseDownloader;

        public GoogleMapsDirectionsResponse(IDownloadResponses webResponseDownloader)
        {
            _webResponseDownloader = webResponseDownloader;
        }

        public GoogleMapsDirectionsResponse()
        {
            _webResponseDownloader = new WebClientWrapper();
        }

        public string Generate(StartingPoint origin, Destination destination)
        {
            string baseUri = "http://maps.googleapis.com/maps/api/directions/";

            string searchTerm = origin.Location.SearchTerm;

            string term = destination.Location.SearchTerm;

            string address = String.Format("{0}json?origin={1}&destination={2}&sensor=false", baseUri,
                                           searchTerm,
                                           term);

            return _webResponseDownloader.Get(address);
        }
    }
}