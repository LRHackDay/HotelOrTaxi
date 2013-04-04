using Newtonsoft.Json;

namespace JourneyCalculator
{
    public interface IDeserialiseGoogleMapsDirectionsResponses
    {
        DirectionsResponse DeserializeResponse(string response);
    }

    public class GoogleMapsApiDeserialiser : IDeserialiseGoogleMapsDirectionsResponses
    {
        public DirectionsResponse DeserializeResponse(string response)
        {
            return JsonConvert.DeserializeObject<DirectionsResponse>(response);
        }
    }
}