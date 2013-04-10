using Configuration;
using WebResponse;

namespace TaxiApi.Request
{
    public class WebClientApiRequest : IPerformApiRequest
    {
        private readonly ICanReadConfigurations _configReader;
        private readonly IDownloadResponses _webResponseReader;

        public WebClientApiRequest(ICanReadConfigurations configReader, IDownloadResponses webResponseReader)
        {
            _configReader = configReader;
            _webResponseReader = webResponseReader;
        }

        public WebClientApiRequest()
        {
            _configReader = new ConfigReader();
            _webResponseReader = new WebClientWrapper();
        }

        public string Perform(string request)
        {
            string formattedRequest = string.Concat(_configReader.TaxiApiUrl(), request);

            return _webResponseReader.Get(formattedRequest);
        }
    }
}