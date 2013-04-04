namespace HotelOrTaxi.Models
{
    public class ResultsViewModel
    {
        public Result Winner { get; set; }
    }

    public class Result
    {
        public string Name { get; set; }

        public string Price { get; set; }

        public string Uri { get; set; }
    }
}