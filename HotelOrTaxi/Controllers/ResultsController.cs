using System.Web.Mvc;
using Geography;
using HotelOrTaxi.Models;
using LateRoomsScraper;
using Results;
using TaxiApi.Configuration;
using TaxiApi.Request;
using TaxiApi.Response;
using WebResponse;

namespace HotelOrTaxi.Controllers
{
    public class ResultsController : Controller
    {
        private readonly ICreateResultViewModels _resultsViewModelFactory;
        private DistanceFactory _distanceFactory;

        public ResultsController(ICreateResultViewModels resultsViewModelFactory)
        {
            _resultsViewModelFactory = resultsViewModelFactory;
            _distanceFactory = new DistanceFactory();
        }

        public ResultsController()
        {
            ICreateTheTaxiControllerUri taxiResultsPage = new CreateTheTaxiControllerUri();
            ICanReadConfigurations canReadConfigurations = new ConfigReader();
            ICreateRequests fareRequestFactory = new FareRequestFactory(canReadConfigurations);
            IDownloadResponses webResponseReader = new WebClientWrapper();
            IPerformApiRequest webClientApiRequest = new WebClientApiRequest(canReadConfigurations, webResponseReader);
            ICreateResponses fareResponseFactory = new FareResponseFactory(webClientApiRequest);
            ICreateTheTaxiResult taxiResultFactory = new TaxiResultFactory(taxiResultsPage, fareRequestFactory, fareResponseFactory);
            IScrapeWebsites websiteScraper = new HotelScraper();
            ICreateTheHotelResult hotelResultFactory = new HotelResultFactory(websiteScraper);

            _resultsViewModelFactory = new ResultsViewModelFactory(taxiResultFactory, hotelResultFactory);
            _distanceFactory = new DistanceFactory();
        }

        public ViewResult Index(string from, string to, string fromlatlong, string tolatlong)
        {
            ICreateResultViewModels resultsViewModelFactory = _resultsViewModelFactory;
            var startingPoint = new StartingPoint(fromlatlong);
            var destination = new Destination(tolatlong);
            var journey = new Journey(startingPoint, destination, _distanceFactory);
            var resultsViewModel = resultsViewModelFactory.Create(Url, journey);

            return View("Index", resultsViewModel);
        }
    }
}