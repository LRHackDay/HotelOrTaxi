namespace Geography
{
    public class Destination
    {
        public Location Location { get; set; }

        public Destination(string searchTerm)
        {
            if (searchTerm != null && searchTerm.Contains(","))
            {
                string[] strings = searchTerm.Split(',');

                var latitude = new Latitude(strings[0]);
                var longitude = new Longitude(strings[1]);

                Location = new Location(latitude, longitude);
            }

            else
                Location = new Location(searchTerm);
        }
    }
}