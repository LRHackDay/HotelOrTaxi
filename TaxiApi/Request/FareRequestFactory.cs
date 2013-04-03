using System;
using System.Text;
using Geography;
using TaxiApi.Configuration;

namespace TaxiApi.Request
{
    public interface ICreateRequests
    {
        string Create(DateTime date, Journey journey);
    }

    public class FareRequestFactory : ICreateRequests
    {
        private readonly IConfiguration _configuration;

        public FareRequestFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Create(DateTime date, Journey journey)
        {
            var request = new StringBuilder();

            request.Append(string.Format("?key={0}", _configuration.ApiKey()));
            request.Append("&type=fare");
            request.Append("&mobile=0");
            request.Append("&return=json");
            request.Append(string.Format("&year={0}", date.Year));
            request.Append(string.Format("&month={0}", date.Month));
            request.Append(string.Format("&day={0}", date.Day));
            request.Append(string.Format("&hour={0}", date.Hour));
            request.Append(string.Format("&minute={0}", date.Minute));
            request.Append(string.Format("&distance={0}", journey.Distance));
            request.Append(string.Format("&passengers={0}", journey.Passengers));
            request.Append(string.Format("&from={0}", journey.StartingPoint));

            return request.ToString();
        }
    }
}