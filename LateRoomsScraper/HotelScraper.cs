using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace LateRoomsScraper
{
    public class HotelScraper : IScrapeWebsites
    {
        private readonly ISaveHotels _hotelStore;
        private const string URL_FORMAT = "http://m.laterooms.com/en/p9827/MobileSearch.aspx?k=&n=1&minp=&maxp=&StarRatingFilter=&rt=2-0&Latitude={0}&Longitude={1}&MaxRadius=10&PageSize=10&toStep=&SortBy=&Ascending=False";
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
            var hotelName = _retrieveElementText.RetrieveNodeText(node, "div/div[2]/div[1]");
            var location = _retrieveElementText.RetrieveNodeText(node, "div/div[2]/span");
            var starRating = _retrieveElementText.RetrieveNodeText(node, "div/div[2]/div[2]");
            var guestRating = _retrieveElementText.RetrieveNodeText(node, "div/div[1]/div[2]/div/div/span");
            var smiley = _retrieveElementText.RetrieveNodeAttribute(node, "div/div[1]/div[2]/div/div/div", "class");
            var numberOfReviews = _retrieveElementText.RetrieveNodeText(node, "div/div[1]/div[2]/div/strong");
            var totalPrice = _retrieveElementText.RetrieveNodeText(node, "div/div[3]/div[2]/div/span/span[2]");
            var url = _retrieveElementText.RetrieveNodeAttribute(node, null, "href");
            var image = _retrieveElementText.RetrieveNodeAttribute(node, "div/div[1]/div[1]/span/img", "src");

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
