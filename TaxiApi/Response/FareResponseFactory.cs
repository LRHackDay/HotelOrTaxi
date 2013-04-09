using Newtonsoft.Json;
using TaxiApi.Request;

namespace TaxiApi.Response
{
    public interface ICreateResponses
    {
        FareResponse Create(string fareRequest);
    }

    public class FareResponseFactory : ICreateResponses
    {
        private readonly IPerformApiRequest _performApiRequest;

        public FareResponseFactory(IPerformApiRequest performApiRequest)
        {
            _performApiRequest = performApiRequest;
        }

        public FareResponseFactory()
        {
            _performApiRequest = new WebClientApiRequest();
        }

        public FareResponse Create(string fareRequest)
        {
            string response = _performApiRequest.Perform(fareRequest);

            return DeserializeResponse(response);
        }

        private static FareResponse DeserializeResponse(string response)
        {
            return JsonConvert.DeserializeObject<FareResponse>(response);
        }
    }

    public class FakeFareResponseFactory : ICreateResponses
    {
        public FareResponse Create(string fareRequest)
        {
            return new FareResponse
                {
                    Fare = new Fare
                        {
                            Cost = (decimal) 30.25
                        }
                };
        }
    }
}