using System.Net;
using TaxiApi.Configuration;
using WebResponse;

namespace TaxiApi.Request
{
    public class WebClientApiRequest : IPerformApiRequest
    {
        private readonly ICanReadConfigurations _configReader;
        private readonly ICanDownloadResponses _webResponseReader;

        public WebClientApiRequest(ICanReadConfigurations configReader, ICanDownloadResponses webResponseReader)
        {
            _configReader = configReader;
            _webResponseReader = webResponseReader;
        }

        public string Perform(string request)
        {
            var formattedRequest = string.Concat(_configReader.ApiUrl(), request);

            return _webResponseReader.DownloadString(formattedRequest);
        }
    }
}