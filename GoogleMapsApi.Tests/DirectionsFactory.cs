using System;
using System.Collections.Generic;
using Geography;
using Newtonsoft.Json;
using WebResponse;

internal class DirectionsFactory
{
    private readonly ICanDownloadResponses _webResponseDownloader;

    public DirectionsFactory(ICanDownloadResponses webResponseDownloaderWrapper)
    {
        _webResponseDownloader = webResponseDownloaderWrapper;
    }

    public Metres GetDistance()
    {
        string address =
            "http://maps.googleapis.com/maps/api/directions/json?origin=Toronto&destination=Montreal&sensor=false";
        string response = _webResponseDownloader.DownloadString(address);

        GoogleMapsDirectionsResponse googleMapsDirectionsResponse = DeserializeResponse(response);
        string actualDistance = googleMapsDirectionsResponse.Routes[0].Legs[0].Distance.Value;
        return new Metres(Decimal.Parse(actualDistance));
    }

    public static GoogleMapsDirectionsResponse DeserializeResponse(string response)
    {
        return JsonConvert.DeserializeObject<GoogleMapsDirectionsResponse>(response);
    }

    public class GoogleMapsDirectionsResponse
    {
        public List<Routes> Routes { get; set; }
    }

    public class Routes
    {
        public List<Legs> Legs { get; set; }
    }

    public class Legs
    {
        public Distance Distance { get; set; }
    }

    public class Distance
    {
        public string Value { get; set; }
    }
}