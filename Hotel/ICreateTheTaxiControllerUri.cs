using System.Web.Mvc;

namespace Results
{
    public interface ICreateTheTaxiControllerUri
    {
        string GetUriForTaxi(UrlHelper url);
    }

    public class CreateTheTaxiControllerUri : ICreateTheTaxiControllerUri
    {
        public string GetUriForTaxi(UrlHelper url)
        {
            return url.Action("Index", "Taxi");
        }
    }
}