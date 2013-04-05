using System;
using System.Collections.Generic;
using System.Net;
using HtmlAgilityPack;

namespace LateRoomsScraper
{
    public class HotelScraper : IScrapeWebsites
    {
        private const string UrlFormat = "http://m.laterooms.com/en/p9827/MobileAjax.aspx?pageSize=10&search=%7B%22Latitude%22:{0},%22Longitude%22:{1},%22Radius%22:1,%22RadiusDistanceUnit%22:%22Miles%22,%22Date%22:%22{2:yyyy}{2:MM}{2:dd}%22,%22CurrencyId%22:%22GBP%22,%22HotelFilter%22:1,%22SortOrder%22:%22TotalPrice%22,%22SortedAscending%22:true,%22Type%22:%22Standard%22,%22PageNumber%22:1,%22Facilities%22:0,%22StarRating%22:0,%22StarRatingBitmap%22:0,%22CustomerRatingBitmap%22:0,%22AppealBitmap%22:0,%22CustomerRatingPercentageFrom%22:0,%22MinPrice%22:0,%22MaxPrice%22:99999999,%22HasSpecialOffers%22:false,%22SpecialOffersBitmap%22:0,%22Nights%22:1%7D";
        private string _latitude;
        private string _longitude;
        private string ScrapeUrl
        {
            get { return string.Format(UrlFormat, _latitude, _longitude, DateTime.Now); }
        }

        public IScraperResponse Scrape(string latitude, string longitude)
        {
            _latitude = latitude;
            _longitude = longitude;
            var hotel = ScrapeFirstHotelFromDocument();
            //var hotels = ScrapeAllHotelsFromDocument();

            return new HotelScraperResponse
                {
                    Hotel = hotel,
                    //Hotels = hotels
                };
        }

        private Hotel ScrapeFirstHotelFromDocument()
        {
            var htmlDocument = GetHtmlDocument();

            var node = htmlDocument.DocumentNode.SelectSingleNode("//*[@id='searchResults']/a[1]/div/div[2]/div[3]/div/span/span[2]");

            return CreateHotelFromRow(node);
        }

        private List<Hotel> ScrapeAllHotelsFromDocument()
        {
            var htmlDocument = GetHtmlDocument();
            var hotels = new List<Hotel>();

            var resultIndex = 1;
            var numberOfResults = 10;
            for (int index = resultIndex; index <= numberOfResults; index++)
            {
                var xpath = string.Format("//*[@id='searchResults']/a[{0}]/div/div[2]/div[3]/div/span/span[2]", index);
                var hotelNameNode = htmlDocument.DocumentNode.SelectSingleNode(xpath);

                hotels.Add(new Hotel
                    {
                        Name = hotelNameNode.InnerText
                    });
            }

            return hotels;
        }

        private Hotel CreateHotelFromRow(HtmlNode htmlNode)
        {
            return new Hotel
                {
                    Url = string.Empty,
                    Name = string.Empty,
                    Price = htmlNode.InnerText.Trim().Substring(2)
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