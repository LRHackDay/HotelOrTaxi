using JourneyCalculator;

public interface ISpecifyConditionsOfNoTaxiRoutesFound
{
    bool IsSatisfiedBy(DirectionsResponse googleMapsDirections);
}

public class SpecifyConditionsOfNoTaxiRoutesFound : ISpecifyConditionsOfNoTaxiRoutesFound
{
    public bool IsSatisfiedBy(DirectionsResponse googleMapsDirections)
    {
        return googleMapsDirections.Routes.Count < 1;
    }
}