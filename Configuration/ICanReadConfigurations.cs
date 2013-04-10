namespace Configuration
{
    public interface ICanReadConfigurations
    {
        string TaxiApiUrl();
        string TaxiApiKey();
        string GooglePlacesApiKey();
    }
}