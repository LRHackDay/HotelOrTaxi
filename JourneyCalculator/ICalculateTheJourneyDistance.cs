using Geography;

namespace JourneyCalculator
{
    public interface ICalculateTheJourneyDistance
    {
        Metres Create(StartingPoint @from, Destination to);
    }
}