using System.Web.Mvc;
using JourneyCalculator;

namespace Results
{
    public interface ICreateTheTaxiResult
    {
        TaxiResult Create(UrlHelper urlHelper, Journey journey);
    }
}