using System;
using Geography;
using TaxiApi.Request;
using TaxiApi.Response;

namespace Results
{
    public interface ICalulateTheTaxiFare
    {
        double GetTaxiPrice(Journey journey);
    }

    public class TaxiFareCalculator : ICalulateTheTaxiFare
    {
        private readonly ICreateRequests _fareRequestFactory;
        private readonly ICreateResponses _fareResponseFactory;

        public TaxiFareCalculator(ICreateRequests fareRequestFactory, ICreateResponses fareResponseFactory)
        {
            _fareRequestFactory = fareRequestFactory;
            _fareResponseFactory = fareResponseFactory;
        }

        public double GetTaxiPrice(Journey journey)
        {
            string request = _fareRequestFactory.Create(DateTime.Now, journey);

            FareResponse fareResponse = _fareResponseFactory.Create(request);

            return (double) fareResponse.Fare.Cost;
        }
    }
}