namespace Geography
{
    public class StartingPoint
    {
        public Location Location { get; set; }

        public StartingPoint(Location location)
        {
            Location = location;
        }

        public StartingPoint(string searchTerm)
        {
            Location = new Location(searchTerm);
        }
    }
}