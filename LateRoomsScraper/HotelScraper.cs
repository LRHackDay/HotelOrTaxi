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
            var documentNode = htmlDocument.DocumentNode;

            const int resultIndex = 1;
            const int numberOfResults = 10;
            for (int index = resultIndex; index <= numberOfResults; index++)
            {
                HtmlNode anchorNode = null;
                anchorNode = documentNode.SelectSingleNode(string.Format("//*[@id='searchResults']/a[{0}]", index));
                var hotel = RetrieveHotel(anchorNode);
                if (hotel.TotalPrice > 0)
                {
                    hotels.Add(hotel);
                }
            }

            return hotels;
        }

        private static Hotel RetrieveHotel(HtmlNode node)
        {
            var hotelName = RetrieveNodeText(node, "div/div[1]/div/div[1]");
            var location = RetrieveNodeText(node, "div/div[1]/div/span");
            var starRating = RetrieveNodeText(node, "div/div[1]/div/div[2]");
            var guestRating = RetrieveNodeText(node, "div/div[2]/div[1]/div/span");
            var smiley = RetrieveNodeAttribute(node, "div/div[2]/div[1]/div/div", "class");
            var numberOfReviews = RetrieveNodeText(node, "div/div[2]/div[1]/strong");
            var totalPrice = RetrieveNodeText(node, "div/div[2]/div[3]/div/span/span[2]");
            var url = RetrieveNodeAttribute(node, "a", "href");
            var image = RetrieveNodeAttribute(node, "div/div[1]/span/img", "src");

            return new Hotel
                {
                    Name = hotelName,
                    Location = location,
                    StarRating = starRating,
                    GuestRating = guestRating,
                    Smiley = smiley,
                    NumberOfReviews = numberOfReviews.Replace("Genuine Reviews", " genuine reviews"),
                    TotalPrice = totalPrice == null ? 0 : double.Parse(totalPrice.Substring(2)),
                    Url = url,
                    ImageSource = image
                };
        }

        private static string RetrieveNodeText(HtmlNode parent, string xPath)
        {
            var node = parent.SelectSingleNode(xPath);
            return node != null ? node.InnerText.Trim() : null;
        }

        private static string RetrieveNodeAttribute(HtmlNode parent, string xPath, string attribute)
        {
            var node = parent.SelectSingleNode(xPath);
            return node != null ? node.Attributes[attribute].Value : null;
        }
    }
}