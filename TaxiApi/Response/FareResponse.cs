namespace TaxiApi.Response
{
    public class FareResponse
    {
        public Fare Fare { get; set; }
        public District District { get; set; }
        public string CallbackId { get; set; }
    }

    public class District
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int Id { get; set; }
        public string Enc { get; set; }
        public string Supported { get; set; }
    }

    public class Fare
    {
        public decimal Cost { get; set; }
        public string Waiting { get; set; }
        public decimal WaitingEstimate { get; set; }
        public string Reason { get; set; }
        public string Warning { get; set; }
        public Tariff Tariff { get; set; }
    }

    public class Tariff
    {
        public string Key { get; set; }
        public int Id { get; set; }
    }
}