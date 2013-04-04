using NUnit.Framework;

namespace LateRoomsScraper.Tests
{
    [TestFixture]
    public class HotelRequests : IScrapeWebsites
    {
        private bool _scrapeCalled;

        [SetUp]
        public void Setup()
        {
            _scrapeCalled = false;
        }

        [Test]
        public void request_returns_hotel_list()
        {
            IScrapeWebsites webScraper = this;

            var scraperResponseFactory = new ScraperResponseFactory(webScraper);

            var response = scraperResponseFactory.Create();

            Assert.That(_scrapeCalled, Is.True);
        }

        public IScraperResponse Scrape()
        {
            _scrapeCalled = true;
            return null;
        }
    }

    public class FakeLateRoomsScraper : IScrapeWebsites
    {
        public IScraperResponse Scrape()
        {
            throw new System.NotImplementedException();
        }
    }
}
