namespace TaxiApi.Request
{
    public class Journey
    {
        public int Distance { get; set; }
        public int Passengers { get; set; }
        public string StartingPoint { get; set; }
    }
}