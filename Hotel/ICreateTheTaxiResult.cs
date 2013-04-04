using System.Web.Mvc;

namespace Results
{
    public interface ICreateTheTaxiResult
    {
        Result Create(UrlHelper urlHelper);
    }
}