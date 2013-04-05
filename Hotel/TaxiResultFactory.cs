using System.Web.Mvc;
using JourneyCalculator;
using WebResponse;

namespace Results
{
    public class TaxiResultFactory : ICreateTheTaxiResult
    {
        private readonly ICreateTheTaxiControllerUri _createTheTaxiControllerUri;
        private readonly TaxiFareCalculator _taxiFareCalculator;

        public TaxiResultFactory(ICreateTheTaxiControllerUri createTheTaxiControllerUri,
                                 TaxiFareCalculator taxiFareCalculator)
        {
            _createTheTaxiControllerUri = createTheTaxiControllerUri;
            _taxiFareCalculator = taxiFareCalculator;
        }

        public TaxiResult Create(UrlHelper urlHelper, Journey journey)
        {
            var taxiResult = new TaxiResult();
            taxiResult.Price = GetTaxiPrice(journey);
            taxiResult.Uri = _createTheTaxiControllerUri.GetUriForTaxi(urlHelper);
            return taxiResult;
        }

        private double GetTaxiPrice(Journey journey)
        {
            try
            {
                double taxiPrice = _taxiFareCalculator.GetTaxiPrice(journey);
                return taxiPrice;
            }
            catch (TaxiApiException)
            {
                return 35.00;
            }
        }
    }
}