namespace Results
{
    public class Result
    {
        public string Name { get; protected set; }

        public double Price { get; set; }

        public string Uri { get; set; }

        public string SubHeadline { get; protected set; }
    }

    public class HotelResult : Result
    {
        public HotelResult()
        {
            Name = "Hotel";
            SubHeadline = "Cheapest price";
        }
    }

    public class TaxiResult : Result
    {
        public TaxiResult()
        {
            Name = "Taxi";
            SubHeadline = "Avg price";
        }
    }
}