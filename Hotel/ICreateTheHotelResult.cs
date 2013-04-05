using Geography;
using JourneyCalculator;

namespace Results
{
    public interface ICreateTheHotelResult
    {
        Result Create(StartingPoint startingPoint);
    }
}