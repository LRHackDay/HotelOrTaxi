using System.Web.Mvc;

namespace Results
{
    public interface ICreateTheTaxiResult
    {
        TaxiResult Create(UrlHelper urlHelper);
    }
}