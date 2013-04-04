﻿using NUnit.Framework;

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

            var response = scraperResponseFactory.Create(null);

            Assert.That(_scrapeCalled, Is.True);
        }

        public IScraperResponse Scrape(string unknown)
        {
            _scrapeCalled = true;
            return null;
        }
    }

    public class FakeLateRoomsScraper : IScrapeWebsites
    {
        public IScraperResponse Scrape(string unknown)
        {
            throw new System.NotImplementedException();
        }
    }
}
