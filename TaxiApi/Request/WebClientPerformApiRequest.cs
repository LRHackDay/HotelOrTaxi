using System.Net;
using TaxiApi.Configuration;

namespace TaxiApi.Request
{
    public class WebClientApiRequest : IPerformApiRequest
    {
        private readonly IConfiguration _configuration;

        public WebClientApiRequest(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Perform(string request)
        {
            var webClient = new WebClient();

            var formattedRequest = string.Concat(_configuration.ApiUrl(), request);

            return webClient.DownloadString(formattedRequest);
        }
    }
}