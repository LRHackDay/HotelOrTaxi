using NUnit.Framework;

namespace Results.Tests
{
    [TestFixture]
    public class TestTaxiPriceCalculator
    {
        [Test]
        public void ReturnsPrice()
        {
            var taxiPriceCalculator = new TaxiFareCalculator();
            Assert.That(taxiPriceCalculator.GetTaxiPrice(), Is.EqualTo(26.00));
        }
    }
}