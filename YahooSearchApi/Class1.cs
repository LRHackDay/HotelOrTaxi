using System.Collections.Generic;
using NUnit.Framework;
using Newtonsoft.Json;
using WebResponse;

namespace YahooSearchApi
{
    /// <summary>
    /// http://developer.yahoo.com/search/local/V3/localSearch.html
    /// </summary>
    [TestFixture]
    public class Class1
    {
        [Test]
        [Ignore]
        public void Bob()
        {
            var webClient = new WebClientWrapper();
            string address =
                "http://local.yahooapis.com/LocalSearchService/V3/localSearch?appid=yahoo_app_id&query=taxi&location=Manchester&results=3&output=json";

            var response =
                webClient.DownloadString(
                    address);

            YahooSearchResponse bob = JsonConvert.DeserializeObject<YahooSearchResponse>(response);
            Assert.That(bob.ResultSet.Result[0].Phone, Is.EqualTo("Simon"));
        }
    }

    public class YahooSearchResponse
    {
        public ResultSet ResultSet { get; set; }
    }

    public class ResultSet
    {
        public List<Result> Result { get; set; }
    }

    public class Result
    {
        public string Title { get; set; }
        public string Phone { get; set; }
    }
}