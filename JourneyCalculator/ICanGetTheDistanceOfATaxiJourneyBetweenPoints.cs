using Geography;

namespace JourneyCalculator
{
    public interface ICanGetTheDistanceOfATaxiJourneyBetweenPoints
    {
        Metres Calculate(StartingPoint origin, Destination destination);
    }
}