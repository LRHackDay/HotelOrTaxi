using System.Collections.Generic;
using Geography;
using Newtonsoft.Json;
using WebResponse;

namespace TaxiFirmDetails
{
    public interface ITaxiFirmFactory
    {
        List<TaxiFirm> Create(Location location);
    }

    public class TaxiFirmFactory : ITaxiFirmFactory
    {
        private readonly IDownloadResponses _webClientWrapper;
        private readonly IConstructGoogleTextSearchRequests _googleTextSearchRequestConstructor;
        private readonly IConstructGooglePlaceRequests _googlePlaceRequestConstructor;

        public TaxiFirmFactory(IConstructGoogleTextSearchRequests googleTextSearchRequestConstructor,
                               IConstructGooglePlaceRequests googlePlaceRequestConstructor,
                               IDownloadResponses webClientWrapper)
        {
            _webClientWrapper = webClientWrapper;
            _googleTextSearchRequestConstructor = googleTextSearchRequestConstructor;
            _googlePlaceRequestConstructor = googlePlaceRequestConstructor;
        }

        public List<TaxiFirm> Create(Location location)
        {
            return new List<TaxiFirm> {new TaxiFirm {Name = "Olympic", Number = "0161 872 4040"}};

            string address = _googleTextSearchRequestConstructor.GetTextSearchRequests(location);
            string response = _webClientWrapper.Get(address);

            var places = JsonConvert.DeserializeObject<GooglePlaces>(response);

            var taxiFirms = new List<TaxiFirm>();

            foreach (var place in places.Results)
            {
                taxiFirms.Add(TaxiFirm(place));
            }

            return taxiFirms;
        }

        private TaxiFirm TaxiFirm(GooglePlacesResults firstGooglePlacesResults)
        {
            string response;
            string companyName = firstGooglePlacesResults.Name;
            string placeReference = firstGooglePlacesResults.Reference;

            string placeRequest = _googlePlaceRequestConstructor.GetPlaceRequest(placeReference);

            response = _webClientWrapper.Get(placeRequest);

            var place = JsonConvert.DeserializeObject<GooglePlace>(response);

            GooglePlaceResult googlePlaceResult = place.Result;
            string formattedPhoneNumber = googlePlaceResult.Formatted_Phone_Number;

            var taxiFirm = new TaxiFirm {Name = companyName, Number = formattedPhoneNumber};
            return taxiFirm;
        }
    }
}