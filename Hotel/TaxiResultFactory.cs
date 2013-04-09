using System.Web.Mvc;
using JourneyCalculator;

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
            var taxiResult = new TaxiResult
                {
                    Price = GetTaxiPrice(journey),
                    Uri = _createTheTaxiControllerUri.GetUriForTaxi(urlHelper, journey.StartingPoint)
                };
            return taxiResult;
        }

        private double GetTaxiPrice(Journey journey)
        {
            return _taxiFareCalculator.GetTaxiPrice(journey);
        }
    }
}