namespace LateRoomsScraper
{
    public interface IScrapeWebsites
    {
        IScraperResponse Scrape(string latitude, string longitude);
    }
}