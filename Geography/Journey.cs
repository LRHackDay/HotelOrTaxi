namespace Geography
{
    public class Journey
    {
        public Journey(StartingPoint @from, Destination to, ICalculateTheJourneyDistance distanceFactory)
        {
            StartingPoint = from.Location;
            Distance = distanceFactory.Create(from, to);
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

        public Destination(Location destination, string searchTerm)
        {
            Location = new Location(searchTerm);
             
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