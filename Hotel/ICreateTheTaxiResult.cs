using System.Web.Mvc;
using Geography;

namespace Results
{
    public interface ICreateTheTaxiResult
    {
        TaxiResult Create(UrlHelper urlHelper, Journey journey);
    }
}