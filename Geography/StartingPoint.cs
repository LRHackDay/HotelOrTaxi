namespace Geography
{
    public class StartingPoint
    {
        public Location Location { get; set; }

        public StartingPoint(Location location, string searchTerm)
        {
            Location = location ?? new Location(searchTerm);
        }
    }
}