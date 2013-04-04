using System.Web.Mvc;
using Geography;
using TaxiApi.Request;
using TaxiApi.Response;

namespace Results
{
    public class TaxiResultFactory : ICreateTheTaxiResult
    {
        private readonly ICreateTheTaxiControllerUri _createTheTaxiControllerUri;
        private readonly ICreateResponses _fareResponseFactory;
        private readonly ICreateRequests _fareRequestFactory;

        public TaxiResultFactory(ICreateTheTaxiControllerUri createTheTaxiControllerUri,
                                 ICreateRequests fareRequestFactory, ICreateResponses fareResponseFactory)
        {
            _createTheTaxiControllerUri = createTheTaxiControllerUri;
            _fareRequestFactory = fareRequestFactory;
            _fareResponseFactory = fareResponseFactory;
        }

        public TaxiResult Create(UrlHelper urlHelper, Journey journey)
        {
            return new TaxiResult
                {
                    Price = new TaxiFareCalculator(_fareRequestFactory, _fareResponseFactory).GetTaxiPrice(journey),
                    Uri = _createTheTaxiControllerUri.GetUriForTaxi(urlHelper)
                };
        }
    }
}