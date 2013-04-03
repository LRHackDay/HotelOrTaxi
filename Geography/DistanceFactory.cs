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
            var distance = new Metres(0);
            return distance;
        }
    }
}