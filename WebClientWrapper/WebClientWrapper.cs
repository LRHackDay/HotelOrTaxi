﻿using System.Net;

namespace WebResponse
{
    public class WebClientWrapper : ICanDownloadResponses
    {
        private readonly WebClient _webClient;

        public WebClientWrapper()
        {
            _webClient = new WebClient();
        }

        public string DownloadString(string address)
        {
            return _webClient.DownloadString(address);
        }
    }
}