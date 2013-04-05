using HotelOrTaxi.Models;
using Results;

namespace HotelOrTaxi
{
    public interface ICalculateTheWinner
    {
        ResultsViewModel Winner(Result taxi, Result hotel);
    }

    public class WhoIsTheWinner : ICalculateTheWinner
    {
        public ResultsViewModel Winner(Result taxi, Result hotel)
        {
            if (taxi.Price < hotel.Price)
            {
                return new ResultsViewModel
                    {
                        Loser = hotel,
                        Winner = taxi,
                        PriceDifference = hotel.Price - taxi.Price
                    };
            }
            return new ResultsViewModel
                {
                    Loser = taxi,
                    Winner = hotel,
                    PriceDifference = taxi.Price - hotel.Price
                };
        }
    }
}