namespace LateRoomsScraper
{
    public interface IScrapeWebsites
    {
        IScraperResponse Scrape(string from);
    }
}