namespace TaxiApi.Request
{
    public class Journey
    {
        public Metres Distance { get; set; }
        public Passengers Passengers { get; set; }
        public Location StartingPoint { get; set; }
    }

    public class Latitude : GeographicCoordinate
    {
        public Latitude(string geographicCoordinate)
            : base(geographicCoordinate)
        {
        }
    }

    public class Longitude : GeographicCoordinate
    {
        public Longitude(string geographicCoordinate)
            : base(geographicCoordinate)
        {
        }
    }

    public class GeographicCoordinate
    {
        private readonly string _geographicCoordinate;

        protected GeographicCoordinate(string geographicCoordinate)
        {
            _geographicCoordinate = geographicCoordinate;
        }

        public override string ToString()
        {
            return _geographicCoordinate;
        }
    }

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

    public class Passengers
    {
        protected bool Equals(Passengers other)
        {
            return NumberOfPassengers == other.NumberOfPassengers;
        }

        public override int GetHashCode()
        {
            return NumberOfPassengers;
        }

        private int NumberOfPassengers { get; set; }

        public Passengers(int numberOfPassengers)
        {
            NumberOfPassengers = numberOfPassengers;
        }

        public override string ToString()
        {
            return NumberOfPassengers.ToString();
        }

        public override bool Equals(object obj)
        {
            return Equals((Passengers) obj);
        }
    }

    public class Metres
    {
        protected bool Equals(Metres other)
        {
            return Distance == other.Distance;
        }

        public override int GetHashCode()
        {
            return Distance.GetHashCode();
        }

        private decimal Distance { get; set; }

        public Metres(decimal distance)
        {
            Distance = distance;
        }

        public override string ToString()
        {
            return Distance.ToString();
        }

        public override bool Equals(object obj)
        {
            return Equals((Metres) obj);
        }
    }
}