using System.Web.Mvc;

namespace Results
{
    public class TaxiResultFactory : ICreateTheTaxiResult
    {
        private readonly ICreateTheTaxiControllerUri _createTheTaxiControllerUri;

        public TaxiResultFactory(ICreateTheTaxiControllerUri createTheTaxiControllerUri)
        {
            _createTheTaxiControllerUri = createTheTaxiControllerUri;
        }

        public Result Create(UrlHelper urlHelper)
        {
            var taxi = new Result();
            taxi.Name = "Taxi";
            taxi.Price = "£26";
            taxi.Uri = _createTheTaxiControllerUri.GetUriForTaxi(urlHelper);
            return taxi;
        }
    }
}