namespace Geography
{
    public class Destination
    {
        public Location Location { get; set; }

        public Destination(string searchTerm)
        {
            Location = new Location(searchTerm);
        }
    }
}