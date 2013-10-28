using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace LateRoomsScraper
{
    public class HotelScraper : IScrapeWebsites
    {
        private readonly ISaveHotels _hotelStore;
        private const string URL_FORMAT = "http://m.laterooms.com/en/p9827/MobileAjax.aspx?pageSize=10&r=0.4179167412221432&search={%22Longitude%22:{1},%22Latitude%22:{0},%22Radius%22:1,%22RadiusDistanceUnit%22:%22Miles%22,%22Date%22:%22{2:yyyy}{2:MM}{2:dd}%22,%22CurrencyId%22:%22GBP%22,%22HotelFilter%22:1,%22SortOrder%22:%22TotalPrice%22,%22SortedAscending%22:true,%22Type%22:%22Standard%22,%22PageNumber%22:1,%22Facilities%22:0,%22StarRating%22:0,%22StarRatingBitmap%22:0,%22CustomerRatingBitmap%22:0,%22AppealBitmap%22:0,%22CustomerRatingPercentageFrom%22:0,%22MinPrice%22:0,%22MaxPrice%22:99999999,%22HasSpecialOffers%22:false,%22SpecialOffersBitmap%22:0,%22Nights%22:1}";
        private string _latitude;
        private string _longitude;
        private readonly IDownloadHtml _downloadHtml;
        private readonly IRetrieveElementText _retrieveElementText;

        public HotelScraper(ISaveHotels hotelStore, IDownloadHtml downloadHtml, IRetrieveElementText retrieveElementText)
        {
            _hotelStore = hotelStore;
            _downloadHtml = downloadHtml;
            _retrieveElementText = retrieveElementText;
        }

        public HotelScraper()
        {
            _hotelStore = new AspNetCache();
            _downloadHtml = new DownloadHtml();
            _retrieveElementText = new HtmlElement();
        }

        private string ScrapeUrl
        {
            get { return string.Format(URL_FORMAT, _latitude, _longitude, DateTime.Now); }
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
            var documentNode = _downloadHtml.GetHtmlDocumentNode(ScrapeUrl);
            var anchorNodes = documentNode.SelectNodes("//*[@id='searchResults']/a");

            var hotels = ScrapeHotels(anchorNodes);

            return hotels;
        }

        private List<Hotel> ScrapeHotels(IEnumerable<HtmlNode> anchorNodes)
        {
            return anchorNodes.Select(RetrieveHotel).Where(hotel => hotel.TotalPrice > 0).ToList();
        }

        private Hotel RetrieveHotel(HtmlNode node)
        {
            var hotelName = _retrieveElementText.RetrieveNodeText(node, "div/div[1]/div/div[1]");
            var location = _retrieveElementText.RetrieveNodeText(node, "div/div[1]/div/span");
            var starRating = _retrieveElementText.RetrieveNodeText(node, "div/div[1]/div/div[2]");
            var guestRating = _retrieveElementText.RetrieveNodeText(node, "div/div[2]/div[1]/div/span");
            var smiley = _retrieveElementText.RetrieveNodeAttribute(node, "div/div[2]/div[1]/div/div", "class");
            var numberOfReviews = _retrieveElementText.RetrieveNodeText(node, "div/div[2]/div[1]/strong");
            var totalPrice = _retrieveElementText.RetrieveNodeText(node, "div/div[2]/div[3]/div/span/span[2]");
            var url = _retrieveElementText.RetrieveNodeAttribute(node, null, "href");
            var image = _retrieveElementText.RetrieveNodeAttribute(node, "div/div[1]/span/img", "src");

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
    }
}
