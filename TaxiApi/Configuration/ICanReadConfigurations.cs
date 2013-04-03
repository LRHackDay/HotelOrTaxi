namespace TaxiApi.Configuration
{
    public interface ICanReadConfigurations
    {
        string ApiUrl();
        string ApiKey();
    }
}