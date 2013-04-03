namespace Geography
{
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