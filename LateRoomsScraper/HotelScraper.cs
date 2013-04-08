using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace LateRoomsScraper
{
    public class HotelScraper : IScrapeWebsites
    {
        private readonly ISaveHotels _hotelStore;
        private const string UrlFormat = "http://m.laterooms.com/en/p9827/MobileAjax.aspx?pageSize=10&search=%7B%22Latitude%22:{0},%22Longitude%22:{1},%22Radius%22:1,%22RadiusDistanceUnit%22:%22Miles%22,%22Date%22:%22{2:yyyy}{2:MM}{2:dd}%22,%22CurrencyId%22:%22GBP%22,%22HotelFilter%22:1,%22SortOrder%22:%22TotalPrice%22,%22SortedAscending%22:true,%22Type%22:%22Standard%22,%22PageNumber%22:1,%22Facilities%22:0,%22StarRating%22:0,%22StarRatingBitmap%22:0,%22CustomerRatingBitmap%22:0,%22AppealBitmap%22:0,%22CustomerRatingPercentageFrom%22:0,%22MinPrice%22:0,%22MaxPrice%22:99999999,%22HasSpecialOffers%22:false,%22SpecialOffersBitmap%22:0,%22Nights%22:1%7D";
        private string _latitude;
        private string _longitude;
        private readonly IDownloadHtml _downloadHtml;

        public HotelScraper(ISaveHotels hotelStore, IDownloadHtml downloadHtml)
        {
            _hotelStore = hotelStore;
            _downloadHtml = downloadHtml;
        }

        private string ScrapeUrl
        {
            get { return string.Format(UrlFormat, _latitude, _longitude, DateTime.Now); }
        }

        public IScraperResponse Scrape(string latitude, string longitude)
        {
            _latitude = latitude;
            _longitude = longitude;
            var hotels = ScrapeAllHotelsFromDocument();

            var resultsGuid = _hotelStore.Add(hotels);

            return new HotelScraperResponse
                {
                    Hotels = hotels,
                    ResultsGuid = resultsGuid
                };
        }

        private List<Hotel> ScrapeAllHotelsFromDocument()
        {
            var htmlDocument = _downloadHtml.GetHtmlDocument(ScrapeUrl);
            var hotels = new List<Hotel>();
            var node = htmlDocument.DocumentNode;

            const int resultIndex = 1;
            const int numberOfResults = 10;
            for (int index = resultIndex; index <= numberOfResults; index++)
            {
                var hotel = RetrieveHotel(index, node);
                if (hotel.TotalPrice > 0)
                {
                    hotels.Add(hotel);
                }
            }

            return hotels;
        }

        private static Hotel RetrieveHotel(int index, HtmlNode node)
        {
            var hotelNameXPath = string.Format("//*[@id='searchResults']/a[{0}]/div/div[1]/div/div[1]", index);
            var hotelNameNode = node.SelectSingleNode(hotelNameXPath);
            var locationXPath = string.Format("//*[@id='searchResults']/a[{0}]/div/div[1]/div/span", index);
            var locationNode = node.SelectSingleNode(locationXPath);
            var starRatingXPath = string.Format("//*[@id='searchResults']/a[{0}]/div/div[1]/div/div[2]", index);
            var starRatingNode = node.SelectSingleNode(starRatingXPath);
            var guestRatingXPath = string.Format("//*[@id='searchResults']/a[{0}]/div/div[2]/div[1]/div/span", index);
            var guestRatingNode = node.SelectSingleNode(guestRatingXPath);
            var smileyXPath = string.Format("//*[@id='searchResults']/a[{0}]/div/div[2]/div[1]/div/div", index);
            var smileyNode = node.SelectSingleNode(smileyXPath);
            var numberOfReviewsXPath = string.Format("//*[@id='searchResults']/a[{0}]/div/div[2]/div[1]/strong", index);
            var numberOfReviewsNode = node.SelectSingleNode(numberOfReviewsXPath);
            var totalPriceXPath = string.Format("//*[@id='searchResults']/a[{0}]/div/div[2]/div[3]/div/span/span[2]", index);
            var totalPriceNode = node.SelectSingleNode(totalPriceXPath);
            var urlXPath = string.Format("//*[@id='searchResults']/a[{0}]", index);
            var urlNode = node.SelectSingleNode(urlXPath);
            var imageXPath = string.Format("//*[@id='searchResults']/a[{0}]/div/div[1]/span/img", index);
            var imageNode = node.SelectSingleNode(imageXPath);

            return new Hotel
                {
                    Name = hotelNameNode.InnerText.Trim(),
                    Location = locationNode.InnerText.Trim(),
                    StarRating = starRatingNode.InnerText.Trim(),
                    GuestRating = guestRatingNode.InnerText.Trim(),
                    Smiley = smileyNode.Attributes["class"].Value,
                    NumberOfReviews = numberOfReviewsNode.InnerText.Trim().Replace("Genuine Reviews", " genuine reviews"),
                    TotalPrice = totalPriceNode == null ? 0 : double.Parse(totalPriceNode.InnerText.Trim().Substring(2)),
                    Url = urlNode.Attributes["href"].Value,
                    ImageSource = imageNode.Attributes["src"].Value
                };
        }
    }
}