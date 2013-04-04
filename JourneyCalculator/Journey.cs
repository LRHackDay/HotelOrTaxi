using Geography;

namespace JourneyCalculator
{
    public class Journey
    {
        public Journey(StartingPoint @from, Destination to, ICanGetTheDistanceOfATaxiJourneyBetweenPoints distanceCalculator)
        {
            StartingPoint = from.Location;
            Distance = distanceCalculator.Calculate(from, to);
        }

        public Metres Distance { get; private set; }
        public Location StartingPoint { get; private set; }
    }
}