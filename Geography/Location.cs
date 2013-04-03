namespace Geography
{
    public class Location
    {
        private string LatitudeAndLongitude { get; set; }

        public Location(string latitudeAndLongitude)
        {
            LatitudeAndLongitude = latitudeAndLongitude;
        }

        public Location(Latitude latitude, Longitude longitude)
        {
            LatitudeAndLongitude = latitude + "," + longitude;
        }

        public override string ToString()
        {
            return LatitudeAndLongitude;
        }
    }
}