using Geography;

namespace JourneyCalculator
{
    public class DistanceFactory : ICalculateTheJourneyDistance
    {
        public Metres Create(StartingPoint @from, Destination to)
        {
            return new Metres(1000);
        }
    }
}