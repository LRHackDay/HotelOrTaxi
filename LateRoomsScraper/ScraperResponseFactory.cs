namespace LateRoomsScraper
{
    public class ScraperResponseFactory
    {
        private readonly IScrapeWebsites _webScraper;

        public ScraperResponseFactory(IScrapeWebsites webScraper)
        {
            _webScraper = webScraper;
        }

        public IScraperResponse Create(string latitude, string longitude)
        {
            return _webScraper.Scrape(latitude, longitude);
        }
    }
}