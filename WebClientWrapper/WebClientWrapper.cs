using System;
using System.Net;

namespace WebResponse
{
    public class WebClientWrapper : IDownloadResponses
    {
        private readonly WebClient _webClient;

        public WebClientWrapper()
        {
            _webClient = new WebClient();
        }

        public string Get(string address)
        {
            try
            {
                return _webClient.DownloadString(address);
            }
            catch (WebException e)
            {
                throw new TaxiApiException("Error requesting address.", e);
            }
            
        }
    }

    [Serializable]
    public class TaxiApiException : Exception
    {
        public TaxiApiException(string errorRequestingTaxiFare, WebException webException)
            : base(errorRequestingTaxiFare, webException)
        {
        }

        public TaxiApiException()
            :base("Problem with API", new WebException())
        {
        }
    }
}