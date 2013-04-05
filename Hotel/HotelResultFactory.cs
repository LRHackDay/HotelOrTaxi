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
                    Uri = "/hotels?id=" + response.ResultsGuid.ToString()
                };

            return hotelResult;
        }

        private static double GetPrice(HotelScraperResponse response)
        {
            return response.Hotels[0] != null ? response.Hotels[0].TotalPrice : 0;
        }
    }
}