namespace Geography
{
    public class Location
    {
        private string LatitudeAndLongitude { get; set; }

        public Location(Latitude latitude, Longitude longitude)
        {
            LatitudeAndLongitude = latitude + "," + longitude;
        }

        public Location(string searchTerm)
        {
            var latitude = 10;
            var longitude = 10;

            LatitudeAndLongitude = latitude + "," + longitude;
        }

        public override string ToString()
        {
            return LatitudeAndLongitude;
        }
    }
}