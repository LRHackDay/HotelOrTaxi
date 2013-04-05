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

        public string DownloadString(string address)
        {
            try
            {
                return _webClient.DownloadString(address);
            }
            catch (WebException e)
            {
                throw new TaxiApiException(e, "Error requesting Taxi Fare");
            }
            
        }
    }

    public class TaxiApiException : Exception
    {
        public TaxiApiException(WebException webException, string errorRequestingTaxiFare)
            : base(errorRequestingTaxiFare, webException)
        {
        }
    }
}