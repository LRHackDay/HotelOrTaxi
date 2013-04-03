namespace Geography
{
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
}