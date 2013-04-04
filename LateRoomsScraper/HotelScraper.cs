using System.Collections.Generic;
using System.Net;
using HtmlAgilityPack;

namespace LateRoomsScraper
{
    public class HotelScraper : IScrapeWebsites
    {
        private readonly string _city;
        private const string UrlFormat = "http://www.laterooms.com/en/Hotels.aspx?k={0}&sb=tp&sd=true";
        private string ScrapeUrl
        {
            get { return string.Format(UrlFormat, _city); }
        }

        public HotelScraper(string city)
        {
            _city = city;
        }

        public IScraperResponse Scrape()
        {
            var hotels = ScrapeHotelsFromDocument();

            return new ScraperResponse
                {
                    Hotels = hotels
                };
        }

        private List<Hotel> ScrapeHotelsFromDocument()
        {
            var htmlDocument = GetHtmlDocument();
            var hotels = new List<Hotel>();

            foreach (var listItem in htmlDocument.DocumentNode.SelectNodes("//*[@id='list']/table/tr[@class != 'head'][position() <= 5]"))
            {
                hotels.Add(CreateHotelFromRow(listItem));
            }

            return hotels;
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