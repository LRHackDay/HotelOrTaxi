namespace Geography
{
    public class Journey
    {
        public Journey(StartingPoint from, Destination to, Metres distance)
        {
            StartingPoint = from.Location;
            Distance = distance;
        }

        public Metres Distance { get; private set; }
        public Location StartingPoint { get; private set; }
    }

    public class Destination
    {
        public Location Location { get; set; }

        public Destination(Location location)
        {
            Location = location;
        }
    }

    public class StartingPoint
    {
        public Location Location { get; set; }

        public StartingPoint(Location location)
        {
            Location = location;
        }
    }
}