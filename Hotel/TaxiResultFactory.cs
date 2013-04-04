using System.Web.Mvc;
using Geography;
using TaxiApi.Configuration;
using TaxiApi.Request;
using TaxiApi.Response;
using WebResponse;

namespace Results
{
    public class TaxiResultFactory : ICreateTheTaxiResult
    {
        private readonly ICreateTheTaxiControllerUri _createTheTaxiControllerUri;

        public TaxiResultFactory(ICreateTheTaxiControllerUri createTheTaxiControllerUri)
        {
            _createTheTaxiControllerUri = createTheTaxiControllerUri;
        }

        public TaxiResult Create(UrlHelper urlHelper, Journey journey)
        {
            ICanReadConfigurations canReadConfigurations = new ConfigReader();
            IDownloadResponses webClientWrapper = new WebClientWrapper();
            IPerformApiRequest webClientApiRequest = new WebClientApiRequest(canReadConfigurations, webClientWrapper);
            return new TaxiResult
                {
                    Price = new TaxiFareCalculator(new FareRequestFactory(canReadConfigurations), new FareResponseFactory(webClientApiRequest)).GetTaxiPrice(journey),
                    Uri = _createTheTaxiControllerUri.GetUriForTaxi(urlHelper)
                };
        }
    }
}