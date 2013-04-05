using System;
using Newtonsoft.Json;
using TaxiApi.Request;
using WebResponse;

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

        public FareResponse Create(string fareRequest)
        {
                var response = _performApiRequest.Perform(fareRequest);

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
                            Cost = (decimal)30.25
                        }
                };
        }
    }
}