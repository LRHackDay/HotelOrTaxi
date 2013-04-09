using System.Web.Mvc;
using Geography;

namespace Results
{
    public interface ICreateTheTaxiControllerUri
    {
        string GetUriForTaxi(UrlHelper url, Location location);
    }

    public class TaxiControllerUriFactory : ICreateTheTaxiControllerUri
    {
        public string GetUriForTaxi(UrlHelper url, Location location)
        {
            return url.Action("Index", "Taxi",  new {latitude = location.Latitude, longitude = location.Longitude });
        }
    }
}