using System;
using Geography;
using LateRoomsScraper;

namespace Results
{
    public class HotelResultFactory : ICreateTheHotelResult
    {
        private readonly IScrapeWebsites _websiteScraper;

        public HotelResultFactory(IScrapeWebsites websiteScraper)
        {
            _websiteScraper = websiteScraper;
        }

        public Result Create(StartingPoint startingPoint)
        {
            var latitude = startingPoint.Location.Latitude.ToString();
            var longitude = startingPoint.Location.Longitude.ToString();

            var response =
                (HotelScraperResponse)
                _websiteScraper.Scrape(latitude,
                                       longitude);

            var hotelResult = new HotelResult
                {
                    Price = GetPrice(response),
                    Uri =
                        string.Format(
                            "http://m.laterooms.com/en/p9827/MobileAjax.aspx?pageSize=10&search=%7B%22Latitude%22:{0},%22Longitude%22:{1},%22Radius%22:1,%22RadiusDistanceUnit%22:%22Miles%22,%22Date%22:%22{2:yyyy}{2:MM}{2:dd}%22,%22CurrencyId%22:%22GBP%22,%22HotelFilter%22:1,%22SortOrder%22:%22TotalPrice%22,%22SortedAscending%22:true,%22Type%22:%22Standard%22,%22PageNumber%22:1,%22Facilities%22:0,%22StarRating%22:0,%22StarRatingBitmap%22:0,%22CustomerRatingBitmap%22:0,%22AppealBitmap%22:0,%22CustomerRatingPercentageFrom%22:0,%22MinPrice%22:0,%22MaxPrice%22:99999999,%22HasSpecialOffers%22:false,%22SpecialOffersBitmap%22:0,%22Nights%22:1%7D",
                            latitude, longitude, DateTime.Now)
                };

            return hotelResult;
        }

        private static double GetPrice(HotelScraperResponse response)
        {
            return response.Hotel != null ? double.Parse(response.Hotel.Price) : 0;
        }
    }
}