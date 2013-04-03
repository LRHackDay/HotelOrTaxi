using System;
using Geography;
using NUnit.Framework;
using TaxiApi.Configuration;
using TaxiApi.Request;

namespace TaxiApi.Tests
{
    [TestFixture]
    public class FareRequestFactoryTests : IConfiguration, ICalculateTheJourneyDistance
    {
        private Metres _distance;

        [Test]
        public void returns_a_fare_request()
        {
            var fareRequestFactory = new FareRequestFactory(this);

            var location = new Location(null, null);
            ICalculateTheJourneyDistance distanceFactory = this;
            var journey = new Journey(new StartingPoint(location), new Destination(location), distanceFactory);
            string fareRequest = fareRequestFactory.Create(DateTime.Now, journey);

            Assert.That(fareRequest, Is.TypeOf(typeof (string)));
        }

        [Test]
        public void sets_the_api_key()
        {
            var fareRequestFactory = new FareRequestFactory(this);

            var location = new Location(null, null);
            var journey = new Journey(new StartingPoint(location), new Destination(location), this);

            string fareRequest = fareRequestFactory.Create(DateTime.Now, journey);

            Assert.That(fareRequest, Is.StringStarting("?key=test"));
        }

        [Test]
        public void sets_the_type_to_fare()
        {
            var fareRequestFactory = new FareRequestFactory(this);

            _distance = new Metres(0);
            var location = new Location(null, null);
            var journey = new Journey(new StartingPoint(location), new Destination(location), this);
            string fareRequest = fareRequestFactory.Create(DateTime.Now, journey);

            Assert.That(fareRequest, Is.StringContaining("&type=fare"));
        }

        [Test]
        public void sets_the_year()
        {
            var fareRequestFactory = new FareRequestFactory(this);

            var date = new DateTime(2013, 1, 1);
            _distance = new Metres(0);
            var location = new Location(null, null);
            var journey = new Journey(new StartingPoint(location), new Destination(location), this);
            string fareRequest = fareRequestFactory.Create(date, journey);

            Assert.That(fareRequest, Is.StringContaining("&year=2013"));
        }

        [Test]
        public void sets_the_month()
        {
            var fareRequestFactory = new FareRequestFactory(this);

            var date = new DateTime(2013, 1, 1);
            var location = new Location(null, null);
            var journey = new Journey(new StartingPoint(location), new Destination(location), this);
            string fareRequest = fareRequestFactory.Create(date, journey);

            Assert.That(fareRequest, Is.StringContaining("&month=1"));
        }

        [Test]
        public void sets_the_day()
        {
            var fareRequestFactory = new FareRequestFactory(this);

            var date = new DateTime(2013, 1, 1);
            _distance = new Metres(0);
            var location = new Location(null, null);
            var journey = new Journey(new StartingPoint(location), new Destination(location), this);
            string fareRequest = fareRequestFactory.Create(date, journey);

            Assert.That(fareRequest, Is.StringContaining("&day=1"));
        }

        [Test]
        public void sets_the_hour()
        {
            var fareRequestFactory = new FareRequestFactory(this);

            var date = new DateTime(2013, 1, 1, 5, 5, 5);
            var location = new Location(null, null);
            var journey = new Journey(new StartingPoint(location), new Destination(location), this);

            string fareRequest = fareRequestFactory.Create(date, journey);

            Assert.That(fareRequest, Is.StringContaining("&hour=5"));
        }

        [Test]
        public void sets_the_minute()
        {
            var fareRequestFactory = new FareRequestFactory(this);

            var date = new DateTime(2013, 1, 1, 5, 5, 5);
            var location = new Location(null, null);
            var journey = new Journey(new StartingPoint(location), new Destination(location), this);
            string fareRequest = fareRequestFactory.Create(date, journey);

            Assert.That(fareRequest, Is.StringContaining("&minute=5"));
        }

        [Test]
        public void sets_the_distance()
        {
            var fareRequestFactory = new FareRequestFactory(this);

            _distance = new Metres(5000);
            var location = new Location(null, null);
            var journey = new Journey(new StartingPoint(location), new Destination(location), this);
            string fareRequest = fareRequestFactory.Create(DateTime.Now, journey);

            Assert.That(fareRequest, Is.StringContaining("&distance=5000"));
        }

        [Test]
        public void sets_the_return_type()
        {
            var fareRequestFactory = new FareRequestFactory(this);

            var location = new Location(null, null);
            var journey = new Journey(new StartingPoint(location), new Destination(location), this);
            string fareRequest = fareRequestFactory.Create(DateTime.Now, journey);

            Assert.That(fareRequest, Is.StringContaining("&return=json"));
        }

        [Test]
        public void sets_the_mobile_type_to_0()
        {
            var fareRequestFactory = new FareRequestFactory(this);

            var location = new Location(null, null);
            var journey = new Journey(new StartingPoint(location), new Destination(location), this);
            string fareRequest = fareRequestFactory.Create(DateTime.Now, journey);

            Assert.That(fareRequest, Is.StringContaining("&mobile=0"));
        }

        [Test]
        public void sets_the_number_of_passengers()
        {
            var fareRequestFactory = new FareRequestFactory(this);

            var location = new Location(null, null);
            var journey = new Journey(new StartingPoint(location), new Destination(location), this);
            string fareRequest = fareRequestFactory.Create(DateTime.Now, journey);

            Assert.That(fareRequest, Is.StringContaining("&passengers=1"));
        }

        [Test]
        public void sets_the_starting_point()
        {
            var fareRequestFactory = new FareRequestFactory(this);

            var latitude = new Latitude("10");
            var longitude = new Longitude("10");

            var location = new Location(latitude, longitude);
            var startingPoint = new StartingPoint(location);
            string searchTerm = "M4 4EW";
            var destination = new Destination(location, searchTerm);
            var journey = new Journey(startingPoint, destination, this);
            string fareRequest = fareRequestFactory.Create(DateTime.Now, journey);

            Assert.That(fareRequest, Is.StringContaining("&from=10,10"));
        }

        string IConfiguration.ApiUrl()
        {
            throw new NotImplementedException();
        }

        public string ApiKey()
        {
            return "test";
        }

        Metres ICalculateTheJourneyDistance.Create(StartingPoint @from, Destination to)
        {
            return _distance;
        }
    }
}