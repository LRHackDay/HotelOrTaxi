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
            return
                "{\"results\" : [{\"formatted_address\" : \"41 Bloom Street, Manchester, United Kingdom\",\"geometry\" : {\"location\" : {\"lat\" : 53.4770480,\"lng\" : -2.2378190}},\"id\" : \"02ab79450269577e5afdca8490cb7a7996c922e4\",\"name\" : \"Manchester Cars\",\"opening_hours\" : {\"open_now\" : true}}],\"status\" : \"OK\"}";
        }

        string IConstructGooglePlaceRequests.GetPlaceRequest(string placeReference)
        {
            return null;
        }
    }
}