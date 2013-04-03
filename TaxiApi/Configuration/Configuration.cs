using System.Configuration;

namespace TaxiApi.Configuration
{
    public class Configuration : IConfiguration
    {
        public string ApiUrl()
        {
            return ConfigurationSetting("YourTaxiApiUri");
        }

        public string ApiKey()
        {
            return ConfigurationSetting("YourTaxiApiKey");
        }

        private static string ConfigurationSetting(string setting)
        {
            return ConfigurationManager.AppSettings[setting];
        }
    }
}