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

        public Result Create(string from)
        {
            var response = (HotelScraperResponse)_websiteScraper.Scrape(from);

            var hotelResult = new HotelResult
                {
                    Price = GetPrice(response),
                    Uri =
                        "http://m.laterooms.com/en/p9827/MobileSearch.aspx?k=Manchester&d=20130404&n=1&adults=1&children=0&minp=&maxp=&StarRatingFilter=&rt=1-0&Latitude=&Longitude=&MaxRadius=1&PageSize=10&toStep=&SortBy=Price&Ascending=True&findButton=FIND+HOTELS&sb=tp&sd=true"
                };
            
            return hotelResult;
        }

        private static double GetPrice(HotelScraperResponse response)
        {
            return response.Hotel != null ? double.Parse(response.Hotel.Price) : 0;
        }
    }
}