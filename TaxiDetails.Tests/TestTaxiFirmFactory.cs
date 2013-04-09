using System.Collections.Generic;
using Geography;
using NUnit.Framework;
using TaxiFirmDetails;

namespace TaxiDetails.Tests
{
    [TestFixture]
    public class TestTaxiFirmFactory : IConstructGoogleTextSearchRequests, IConstructGooglePlaceRequests
    {
        [Test]
        [Ignore]
        public void ReturnsNameAndNumber()
        {
            var longitude = new Longitude("-2.240117");
            var latitude = new Latitude("53.477716");

            var taxiFirmRepository = new TaxiFirmFactory(this, this);
            Location location = new Location(latitude, longitude);
            List<TaxiFirm> taxiFirms = taxiFirmRepository.Create(location);

            Assert.That(taxiFirms[0].Name, Is.EqualTo("Manchester Cars"));
            Assert.That(taxiFirms[0].Number, Is.EqualTo("0161 228 3355"));
        }

        string IConstructGoogleTextSearchRequests.GetTextSearchRequests(Location location)
        {
            throw new System.NotImplementedException();
        }

        string IConstructGooglePlaceRequests.GetPlaceRequest(string placeReference)
        {
            throw new System.NotImplementedException();
        }
    }
}