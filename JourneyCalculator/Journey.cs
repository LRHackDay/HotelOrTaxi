using Geography;

namespace JourneyCalculator
{
    public class Journey
    {
        public Journey(StartingPoint @from, Metres distance)
        {
            StartingPoint = from.Location;
            Distance = distance;
        }

        public Metres Distance { get; private set; }
        public Location StartingPoint { get; private set; }
    }
}