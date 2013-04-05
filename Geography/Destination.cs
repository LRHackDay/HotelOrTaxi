namespace Geography
{
    public class Destination
    {
        public Location Location { get; set; }

        public Destination(Location location, string searchTerm)
        {
            Location = location ?? new Location(searchTerm);
        }
    }
}