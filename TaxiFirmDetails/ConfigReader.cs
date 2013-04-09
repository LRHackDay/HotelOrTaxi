using System.Configuration;

namespace TaxiFirmDetails
{
    public class ConfigReader : ICanReadConfigurations
    {
        public string ApiKey()
        {
            return ConfigurationSetting("GooglePlacesApiKey");
        }

        private static string ConfigurationSetting(string setting)
        {
            return ConfigurationManager.AppSettings[setting];
        }
    }
}