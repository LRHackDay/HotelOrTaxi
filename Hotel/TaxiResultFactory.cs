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

        public TaxiResultFactory()
        {
            _createTheTaxiControllerUri = new TaxiControllerUriFactory();
            _taxiFareCalculator = new TaxiFareCalculator();
        }

        public TaxiResult Create(UrlHelper urlHelper, Journey journey)
        {
            try
            {
                return new TaxiResult
                    {
                        Price = GetTaxiPrice(journey),
                        Uri = _createTheTaxiControllerUri.GetUriForTaxi(urlHelper, journey.StartingPoint)
                    };
            }
            catch (TaxiApiException)
            {
                return null;
            }
        }

        private double GetTaxiPrice(Journey journey)
        {
            return _taxiFareCalculator.GetTaxiPrice(journey);
        }
    }
}