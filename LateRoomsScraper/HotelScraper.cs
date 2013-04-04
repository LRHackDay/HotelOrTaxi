using System.Collections.Generic;
using System.Net;
using HtmlAgilityPack;

namespace LateRoomsScraper
{
    public class HotelScraper : IScrapeWebsites
    {
        private const string UrlFormat = "http://www.laterooms.com/en/Hotels.aspx?k={0}&sb=tp&sd=true";
        private string _from;
        private string ScrapeUrl
        {
            get { return string.Format(UrlFormat, _from); }
        }

        public IScraperResponse Scrape(string from)
        {
            _from = from;
            var hotel = ScrapeFirstHotelFromDocument();

            return new HotelScraperResponse
                {
                    Hotel = hotel
                };
        }

        private Hotel ScrapeFirstHotelFromDocument()
        {
            var htmlDocument = GetHtmlDocument();

            var node = htmlDocument.DocumentNode.SelectSingleNode("//*[@id='list']/table/tr[@class != 'head'][position() <= 1]");

            return CreateHotelFromRow(node);
        }

        private Hotel CreateHotelFromRow(HtmlNode htmlNode)
        {
            var name = htmlNode.SelectSingleNode("//tr/td[@class = 'sec hname']/div/p/a").InnerText.Trim();
            var url = htmlNode.SelectSingleNode("//tr/td[@class = 'sec hname']/div/p/a").Attributes["href"].Value;
            var price = htmlNode.SelectSingleNode("//tr/td[@class = 'last']/div/b").InnerText.Trim();

            return new Hotel
                {
                    Url = url,
                    Name = name,
                    Price = price
                };
        }

        private HtmlDocument GetHtmlDocument()
        {
            var webClient = new WebClient();
            var url = ScrapeUrl;
            var html = webClient.DownloadString(url);
            
            var document = new HtmlDocument();
            document.LoadHtml(html);

            return document;
        }
    }
}