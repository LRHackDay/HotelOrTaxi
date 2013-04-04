using System.Web.Mvc;

namespace Results
{
    public class TaxiResultFactory : ICreateTheTaxiResult
    {
        private readonly ICreateTheTaxiControllerUri _createTheTaxiControllerUri;

        public TaxiResultFactory(ICreateTheTaxiControllerUri createTheTaxiControllerUri)
        {
            _createTheTaxiControllerUri = createTheTaxiControllerUri;
        }

        public TaxiResult Create(UrlHelper urlHelper)
        {
            return new TaxiResult
                {
                    Price = 26.00,
                    Uri = _createTheTaxiControllerUri.GetUriForTaxi(urlHelper)
                };
        }
    }
}