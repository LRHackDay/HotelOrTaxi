using JourneyCalculator;

public class GoogleMapsDirectionsDistanceMapper
{
    private readonly ISpecifyConditionsOfNoTaxiRoutesFound _specifyConditionsOfNoTaxiRoutesFound;

    public GoogleMapsDirectionsDistanceMapper(ISpecifyConditionsOfNoTaxiRoutesFound specifyConditionsOfNoTaxiRoutesFound)
    {
        _specifyConditionsOfNoTaxiRoutesFound = specifyConditionsOfNoTaxiRoutesFound;
    }

    
}