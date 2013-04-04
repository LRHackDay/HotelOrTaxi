namespace Geography
{
    public interface ICalculateTheJourneyDistance
    {
        Metres Create(StartingPoint @from, Destination to);
    }

    public class DistanceFactory : ICalculateTheJourneyDistance
    {
        public Metres Create(StartingPoint @from, Destination to)
        {
            return new Metres(1000);
        }
    }
}