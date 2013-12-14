using System;
using System.Collections.Generic;
using System.Configuration;
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
                TaxiFirm taxiFirm = TaxiFirm(place);
                if (!IsExclusionPhrase(taxiFirm.Name))
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

            var taxiFirm = new TaxiFirm { Name = companyName, Number = formattedPhoneNumber };
            return taxiFirm;
        }

        private IList<string> ExclusionPhrases(bool exceptions = false)
        {
            IList<string> exclusionPhrases = new List<string>();
            string exclusionPhraseAppKey = exceptions ? "GooglePlacesExclusionPhraseExceptions" : "GooglePlacesExclusionPhrases";
            if (ConfigurationManager.AppSettings[exclusionPhraseAppKey] != null)
            {
                string exclusionPhrasesString = ConfigurationManager.AppSettings[exclusionPhraseAppKey];
                try
                {
                    exclusionPhrases = new List<string>(exclusionPhrasesString.Split(','));

                }
                catch { }
            }
            return exclusionPhrases;
        }

        private bool IsExclusionPhrase(string taxiName)
        {
            bool ret = false;
            foreach (var exclusionPhrase in ExclusionPhrases())
            {
                if (taxiName.ToLower().IndexOf(exclusionPhrase, StringComparison.Ordinal) > -1)
                {
                    foreach (var exclusionPhraseException in ExclusionPhrases(true))
                    {
                        if (taxiName.ToLower().IndexOf(exclusionPhraseException, StringComparison.Ordinal) > -1)
                            return false;
                    }
                    return true;
                }
            }
            return ret;
        }
    }
}