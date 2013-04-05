using System.Web.Mvc;
using Geography;
using HotelOrTaxi.Models;
using JourneyCalculator;
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
        private readonly ICanGetTheDistanceOfATaxiJourneyBetweenPoints _distanceCalculator;

        public ResultsController(ICreateResultViewModels resultsViewModelFactory,
                                 ICanGetTheDistanceOfATaxiJourneyBetweenPoints distanceCalculator)
        {
            _resultsViewModelFactory = resultsViewModelFactory;
            _distanceCalculator = distanceCalculator;
        }

        public ResultsController()
        {
            ICreateTheTaxiControllerUri taxiResultsPage = new CreateTheTaxiControllerUri();
            ICanReadConfigurations canReadConfigurations = new ConfigReader();
            ICreateRequests fareRequestFactory = new FareRequestFactory(canReadConfigurations);
            IDownloadResponses webResponseReader = new WebClientWrapper();
            IPerformApiRequest webClientApiRequest = new WebClientApiRequest(canReadConfigurations, webResponseReader);
            //ICreateResponses fareResponseFactory = new FareResponseFactory(webClientApiRequest);
            ICreateResponses fareResponseFactory = new FakeFareResponseFactory();
            ICreateTheTaxiResult taxiResultFactory = new TaxiResultFactory(taxiResultsPage, fareRequestFactory,
                                                                           fareResponseFactory);
            IGetTheResponseFromGoogleMapsDirectionsApi googleMapsDirectionsResponse =
                new GoogleMapsDirectionsResponse(webResponseReader);
            IDeserialiseGoogleMapsDirectionsResponses googleMapsApiDeserialiser = new GoogleMapsApiDeserialiser();
            ISpecifyConditionsOfNoTaxiRoutesFound specifyConditionsOfNoTaxiRoutesFound =
                new SpecifyConditionsOfNoTaxiRoutesFound();
            IScrapeWebsites websiteScraper = new HotelScraper();
            ICreateTheHotelResult hotelResultFactory = new HotelResultFactory(websiteScraper);

            _distanceCalculator = new DistanceCalculator(googleMapsDirectionsResponse, googleMapsApiDeserialiser,
                                                         specifyConditionsOfNoTaxiRoutesFound);
            _resultsViewModelFactory = new ResultsViewModelFactory(taxiResultFactory, hotelResultFactory);
        }

        public ViewResult Index(string from, string to, string fromlatlong, string tolatlong)
        {
            ICreateResultViewModels resultsViewModelFactory = _resultsViewModelFactory;

            var startingPoint = new StartingPoint(fromlatlong);
            var destination = new Destination(tolatlong);


            var journey = new Journey(startingPoint, destination, _distanceCalculator);

            ResultsViewModel resultsViewModel = resultsViewModelFactory.Create(Url, journey);

            return View("Index", resultsViewModel);
        }

        public ViewResult Fight()
        {
            return View();
        }
    }
}