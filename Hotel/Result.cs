namespace Results
{
    public class Result
    {
        public string Name { get; protected set; }

        public double Price { get; set; }

        public string Uri { get; set; }
    }

    public class HotelResult : Result
    {
        public HotelResult()
        {
            Name = "Hotel";
        }
    }

    public class TaxiResult : Result
    {
        public TaxiResult()
        {
            Name = "Taxi";
        }
    }
}