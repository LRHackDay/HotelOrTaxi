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

        public HotelResultFactory()
        {
            _websiteScraper = new HotelScraper();
        }

        public HotelResult Create(StartingPoint startingPoint)
        {
            try
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
            catch (Exception e)
            {
                return null;
                throw new NoHotelFoundException("No hotel found", e);

            }
        }

        private static double GetPrice(HotelScraperResponse response)
        {
            return response.Hotels[0] != null ? response.Hotels[0].TotalPrice : 0;
        }
    }

    public class NoHotelFoundException : Exception
    {
        public NoHotelFoundException(string message, Exception exception)
            :base(message, exception)
        {   
        }

        public NoHotelFoundException()
            : base("No hotel found", new Exception())
        {
        }
    }
}