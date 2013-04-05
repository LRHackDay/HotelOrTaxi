using Results;

namespace HotelOrTaxi.Models
{
    public class ResultsViewModel
    {
        public ResultsViewModel()
        {
            HasErrors = false;
        }
        public Result Winner { get; set; }

        public Result Loser { get; set; }

        public double PriceDifference { get; set; }

        public bool HasErrors { get; set; }

        public object Error { get; set; }
    }
}