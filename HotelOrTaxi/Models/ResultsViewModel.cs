using Results;

namespace HotelOrTaxi.Models
{
    public class ResultsViewModel
    {
        public Result Winner { get; set; }

        public Result Loser { get; set; }

        public double PriceDifference { get; set; }
    }
}