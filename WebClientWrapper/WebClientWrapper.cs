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
                throw new TaxiApiException("Error requesting Taxi Fare. We've probably run out of api requests.", e);
            }
            
        }
    }

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