using System.Collections.Generic;
using Geography;
using NUnit.Framework;
using TaxiFirmDetails;
using WebResponse;

namespace TaxiDetails.Tests
{
    [TestFixture]
    public class TestTaxiFirmFactory : ICanReadConfigurations
    {
        [Test]
        public void ReturnsNameAndNumber()
        {
            var longitude = new Longitude("-2.240117");
            var latitude = new Latitude("53.477716");

            ICanReadConfigurations configReader = this;
            var taxiFirmRepository = new TaxiFirmFactory(new GoogleTextSearchRequestConstructor(configReader),
                                                            new GooglePlaceRequestConstructor(configReader),
                                                            new WebClientWrapper());
            Location location = new Location(latitude, longitude);
            List<TaxiFirm> taxiFirms = taxiFirmRepository.Create(location);

            Assert.That(taxiFirms[0].Name, Is.EqualTo("Manchester Cars"));
            Assert.That(taxiFirms[0].Number, Is.EqualTo("0161 228 3355"));
        }

        public string ApiKey()
        {
            return "AIzaSyAGpY8q9gQnZl4VhdX9u7Twm_YEdUhvCsc";
        }
    }
}