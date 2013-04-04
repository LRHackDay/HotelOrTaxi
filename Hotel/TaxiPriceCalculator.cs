namespace Results
{
    public interface ICalulateTheTaxiFare
    {
        double GetTaxiPrice();
    }

    public class TaxiFareCalculator : ICalulateTheTaxiFare
    {
        public double GetTaxiPrice()
        {
            return 26.00;
        }
    }
}