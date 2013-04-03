namespace TaxiApi.Request
{
    public class Journey
    {
        public Metres Distance { get; set; }
        public Passengers Passengers { get; set; }
        public string StartingPoint { get; set; }
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
            return Equals((Passengers)obj);
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
            return Equals((Metres)obj);
        }
    }
}