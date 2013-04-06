using System;
using System.Threading;
using HotelOrTaxi.Models;
using Results;

namespace HotelOrTaxi
{
    public interface ICalculateTheWinner
    {
        ResultsViewModel Fight(TaxiResult taxi, HotelResult hotel);
    }

    public class WhoIsTheWinner : ICalculateTheWinner
    {
        public ResultsViewModel Fight(TaxiResult taxi, HotelResult hotel)
        {
            ResultsViewModel result;
            if (taxi == null && hotel != null)
            {
                result = new HotelWins
                    {
                        Loser = null,
                        Winner = hotel,
                        PriceDifference = 0.00,
                        LoserText = "We weren't able to find you a taxi right now."
                    };
            }
            else if (hotel == null && taxi != null)
            {
                result = new TaxiWins
                    {
                        Loser = null,
                        Winner = taxi,
                        PriceDifference = 0.0,
                        LoserText = "We weren't able to find a hotel within a mile of you."
                    };
            }
            
            else if (IsDraw(taxi, hotel))
            {
                result = Draw(taxi, hotel);
            }
            else if (TaxiIsCheaper(taxi, hotel))
            {
                result = TaxiWins(taxi, hotel);
            }
            else {result = HotelWins(taxi, hotel);}
            return result;
        }

        private static bool TaxiIsCheaper(TaxiResult taxi, HotelResult hotel)
        {
            return taxi.Price < hotel.Price;
        }

        private static bool IsDraw(TaxiResult taxi, HotelResult hotel)
        {
            return taxi.Price == hotel.Price;
        }

        private static ResultsViewModel Draw(TaxiResult taxi, HotelResult hotel)
        {
            return new ItWasADraw
                {
                    Loser = taxi,
                    Winner = hotel,
                    PriceDifference = taxi.Price - hotel.Price
                };
        }

        private static ResultsViewModel TaxiWins(Result taxi, Result hotel)
        {
            return new TaxiWins
                {
                    Loser = hotel,
                    Winner = taxi,
                    PriceDifference = hotel.Price - taxi.Price
                };
        }

        private static ResultsViewModel HotelWins(TaxiResult taxi, HotelResult hotel)
        {
            var resultsViewModel = new HotelWins
                {
                    Loser = taxi,
                    Winner = hotel,
                    PriceDifference = taxi.Price - hotel.Price
                };

            return resultsViewModel;
        }
    }

    public class NoClearWinnerExeption : Exception
    {   
        public NoClearWinnerExeption():
            base("No clear winner.")
        {
            
        }
    }

    public class HotelWins : ResultsViewModel
    {
        public HotelWins()
        {
            Animation = "hotel";
        }
    }

    public class TaxiWins : ResultsViewModel
    {
        public TaxiWins()
        {
            Animation = "taxi";
        }
    }

    public class ItWasADraw : ResultsViewModel
    {
        public ItWasADraw()
        {
            IsDraw = true;
            Animation = "draw";
        }
    }
}