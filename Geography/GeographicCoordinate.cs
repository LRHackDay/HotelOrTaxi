namespace Geography
{
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
}