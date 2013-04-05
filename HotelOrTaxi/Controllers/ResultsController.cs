using System;
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
        private LocationFactory _locationFactory;

        public ResultsController(ICreateResultViewModels resultsViewModelFactory,
                                 ICanGetTheDistanceOfATaxiJourneyBetweenPoints distanceCalculator)
        {
            _resultsViewModelFactory = resultsViewModelFactory;
            _distanceCalculator = distanceCalculator;
            _locationFactory = new LocationFactory();
        }

        public ResultsController()
        {
            ICreateTheTaxiControllerUri taxiResultsPage = new CreateTheTaxiControllerUri();
            ICanReadConfigurations canReadConfigurations = new ConfigReader();
            ICreateRequests fareRequestFactory = new FareRequestFactory(canReadConfigurations);
            IDownloadResponses webResponseReader = new WebClientWrapper();
            IPerformApiRequest webClientApiRequest = new WebClientApiRequest(canReadConfigurations, webResponseReader);
            ICreateResponses fareResponseFactory = new FareResponseFactory(webClientApiRequest);
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
            _locationFactory = new LocationFactory();
        }

        public ViewResult Index(string from, string to, string fromlatlong, string tolatlong)
        {
            var resultsViewModel = new ResultsViewModel();
            try
            {
                var startingPoint = new StartingPoint(_locationFactory.GetLocation(fromlatlong), from);
                var destination = new Destination(_locationFactory.GetLocation(tolatlong), to);

                var journey = new Journey(startingPoint, destination, _distanceCalculator);

                resultsViewModel = _resultsViewModelFactory.Create(Url, journey);
            }
            catch (Exception e)
            {
                resultsViewModel.HasErrors = true;
                resultsViewModel.Error = e.Message;
            }

            return View("Index", resultsViewModel);
        }

        public ViewResult Fight()
        {
            return View();
        }
    }

    public class LocationFactory
    {
        public Location GetLocation(string latlong)
        {
            if (latlong != null && latlong.Contains(","))
            {
                string[] strings = latlong.Split(',');

                var latitude = new Latitude(strings[0]);
                var longitude = new Longitude(strings[1]);

                return new Location(latitude, longitude);
            }
            return null;
        }
    }
}