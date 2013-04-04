using System;
using System.Web.Mvc;
using Geography;
using TaxiApi.Configuration;
using TaxiApi.Request;
using TaxiApi.Response;
using WebResponse;

namespace HotelOrTaxi.Controllers
{
    public class ResultsController : Controller
    {
        public ActionResult Index(string from, string to, string fromlatlong, string tolatlong)
        {
            var webClientWrapper = new WebClientWrapper();
            var canReadConfigurations = new CanReadConfigurations();
            var webClientApiRequest = new WebClientApiRequest(canReadConfigurations, webClientWrapper);
            var responseFactory = new FareResponseFactory(webClientApiRequest);
            var journey = new Journey(new StartingPoint(fromlatlong), new Destination(tolatlong), new DistanceFactory());
            var fareRequestFactory = new FareRequestFactory(canReadConfigurations);
            var fareRequest = fareRequestFactory.Create(DateTime.Now, journey);

            var fareResponse = responseFactory.Create(fareRequest);
            //fareResponse.Fare.

            return View();
        }
    }
}