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
            if (IsDraw(taxi, hotel))
            {
                return CreateDraw(taxi, hotel);
            }
            if (TaxiIsCheaper(taxi, hotel))
            {
                return CreateTaxiWinner(taxi, hotel);
            }
            return CreateHotelWinner(taxi, hotel);
        }

        private static bool TaxiIsCheaper(TaxiResult taxi, HotelResult hotel)
        {
            return taxi.Price < hotel.Price;
        }

        private static bool IsDraw(TaxiResult taxi, HotelResult hotel)
        {
            return taxi.Price == hotel.Price;
        }

        private static ResultsViewModel CreateDraw(TaxiResult taxi, HotelResult hotel)
        {
            return new ItWasADraw
                {
                    Loser = taxi,
                    Winner = hotel,
                    PriceDifference = taxi.Price - hotel.Price
                };
        }

        private static ResultsViewModel CreateTaxiWinner(Result taxi, Result hotel)
        {
            return new TaxiWins
                {
                    Loser = hotel,
                    Winner = taxi,
                    PriceDifference = hotel.Price - taxi.Price
                };
        }

        private static ResultsViewModel CreateHotelWinner(TaxiResult taxi, HotelResult hotel)
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