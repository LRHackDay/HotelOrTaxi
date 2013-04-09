using System;
using Geography;
using Results;

namespace HotelOrTaxi.Models
{
    public class ResultsViewModel
    {
        public Result Winner { get; set; }

        public Result Loser { get; set; }

        public double PriceDifference { get; set; }

        public Exception Error { get; set; }

        public bool IsDraw { get; protected set; }

        public string Animation { get; protected set; }

        public string LoserText { get; set; }

        public bool Upsell {
            get { return Loser.Name == "Hotel" && PriceDifference < 25; }
        }
    }
}