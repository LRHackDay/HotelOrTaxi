namespace Geography
{
    public class Journey
    {
        public Journey(StartingPoint from, Destination to)
        {
            StartingPoint = from.Location;
        }

        public Journey()
        {
            
        }

        public Metres Distance { get; set; }
        public Passengers Passengers { get; set; }
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