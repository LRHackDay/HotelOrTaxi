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
            var location = new Location(latitude, longitude);
            List<TaxiFirm> taxiFirms = taxiFirmRepository.Create(location);

            Assert.That(taxiFirms[0].Name, Is.EqualTo("Manchester Cars"));
            Assert.That(taxiFirms[0].Number, Is.EqualTo("0161 228 3355"));
        }

        public string GetTextSearchRequests(Location location)
        {
            return
                "{\"results\" : [{\"formatted_address\" : \"41 Bloom Street, Manchester, United Kingdom\",\"geometry\" : {\"location\" : {\"lat\" : 53.4770480,\"lng\" : -2.2378190}},\"id\" : \"02ab79450269577e5afdca8490cb7a7996c922e4\",\"name\" : \"Manchester Cars\",\"opening_hours\" : {\"open_now\" : true}}],\"status\" : \"OK\"}";
        }

        public string GetPlaceRequest(string placeReference)
        {
            return "{\"result\" : {\"formatted_address\" : \"41 Bloom Street, Manchester, United Kingdom\",\"formatted_phone_number\" : \"0161 228 3355\",\"id\" : \"02ab79450269577e5afdca8490cb7a7996c922e4\",\"international_phone_number\" : \"+44 161 228 3355\",\"name\" : \"Manchester Cars\",\"reference\" : \"CnRtAAAAjnz16RphOCC0HPj6Uz4T3_UcehjYzZc9lx6K8609W8LTtC25xosND-C5aoy1czWRscDKKnLYcNiRs2vHnPzuK8Jq-S9nraXmQTxmZwwdDbl7Fc_SESVm3l6WdgjwOvHrN8t2ceo97U7d10htqGp-8xIQyOvdicRyiTStsq10taaK-hoUyz5KZFUQCfNLePav08TkERTYeCE\"}}";
        }
    }
}