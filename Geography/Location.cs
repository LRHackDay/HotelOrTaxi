namespace Geography
{
    public class Location
    {
        public string SearchTerm { get; set; }
        private string LatitudeAndLongitude { get; set; }

        public Latitude Latitude { get; set; }

        public Location(Latitude latitude, Longitude longitude)
        {
            Latitude = latitude;
            Longitude = longitude;

            LatitudeAndLongitude = latitude + "," + longitude;
        }

        public Longitude Longitude { get; set; }

        public Location(string searchTerm)
        {
            SearchTerm = searchTerm;
        }

        public override string ToString()
        {
            return LatitudeAndLongitude;
        }
    }
}