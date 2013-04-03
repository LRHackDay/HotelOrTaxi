namespace Geography
{
    public class Journey
    {
        public Journey(StartingPoint @from, Destination to, ICalculateTheJourneyDistance distanceFactory)
        {
            StartingPoint = from.Location;
            Distance = distanceFactory.Create(from, to);
        }

        public Metres Distance { get; private set; }
        public Location StartingPoint { get; private set; }
    }
}