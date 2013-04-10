using System.Collections.Generic;
using Geography;
using Newtonsoft.Json;

namespace TaxiFirmDetails
{
    public interface ITaxiFirmFactory
    {
        List<TaxiFirm> Create(Location location);
    }

    public class TaxiFirmFactory : ITaxiFirmFactory
    {
        private readonly IConstructGoogleTextSearchRequests _googleTextSearchRequestConstructor;
        private readonly IConstructGooglePlaceRequests _googlePlaceRequestConstructor;

        public TaxiFirmFactory(IConstructGoogleTextSearchRequests googleTextSearchRequestConstructor,
                               IConstructGooglePlaceRequests googlePlaceRequestConstructor)
        {
            _googleTextSearchRequestConstructor = googleTextSearchRequestConstructor;
            _googlePlaceRequestConstructor = googlePlaceRequestConstructor;
        }

        public List<TaxiFirm> Create(Location location)
        {
            string response = _googleTextSearchRequestConstructor.GetTextSearchRequests(location);

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
            string companyName = firstGooglePlacesResults.Name;
            string placeReference = firstGooglePlacesResults.Reference;

            string response = _googlePlaceRequestConstructor.GetPlaceRequest(placeReference);

            var place = JsonConvert.DeserializeObject<GooglePlace>(response);

            GooglePlaceResult googlePlaceResult = place.Result;
            string formattedPhoneNumber = googlePlaceResult.Formatted_Phone_Number;

            var taxiFirm = new TaxiFirm {Name = companyName, Number = formattedPhoneNumber};
            return taxiFirm;
        }
    }
}