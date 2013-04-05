using System;
using Geography;
using JourneyCalculator;
using NUnit.Framework;
using TaxiApi.Request;
using TaxiApi.Response;
namespace Results.Tests
{
    [TestFixture]
    public class TestTaxiPriceCalculator : ICreateResponses, ICreateRequests
    {
        private FareResponse _fareResponse;

        public TestTaxiPriceCalculator(FareResponse fareResponse)
        {
            _fareResponse = fareResponse;
        }

        [Test]
        public void ReturnsPrice()
        {
            const decimal expectedPrice = (decimal) 26.00;
            var fare = new Fare {Cost = expectedPrice};
            _fareResponse = new FareResponse
                {
                    Fare = fare
                };

            var startingPoint = new StartingPoint(null, "bob");
            var destination = new Destination(null, "bob");
            var journey = new Journey(startingPoint, destination, null);


            ICreateRequests jane = this;
            ICreateResponses bob = this;
            var taxiPriceCalculator = new TaxiFareCalculator(jane, bob);
            Assert.That(taxiPriceCalculator.GetTaxiPrice(journey), Is.EqualTo(expectedPrice));
        }

        public Metres Create(StartingPoint @from, Destination to)
        {
            return new Metres(0);
        }


        FareResponse ICreateResponses.Create(string fareRequest)
        {
            return _fareResponse;
        }

        string ICreateRequests.Create(DateTime date, Journey journey)
        {
            return null;
        }
    }
}