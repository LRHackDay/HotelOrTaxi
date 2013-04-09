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
        private readonly ICreateLocations _createLocations;

        public ResultsController(ICreateResultViewModels resultsViewModelFactory, ICreateLocations createLocations)
        {
            _resultsViewModelFactory = resultsViewModelFactory;
            _createLocations = createLocations;
        }

        public ResultsController()
        {
            var taxiResultsPage = new CreateTheTaxiControllerUri();
            var canReadConfigurations = new ConfigReader();
            var fareRequestFactory = new FareRequestFactory(canReadConfigurations);
            var webResponseReader = new WebClientWrapper();
            var performApiRequest = new WebClientApiRequest(canReadConfigurations, webResponseReader);
            var fareResponseFactory = new FareResponseFactory(performApiRequest);
            var taxiFareCalculator = new TaxiFareCalculator(fareRequestFactory, fareResponseFactory);
            var taxiResultFactory = new TaxiResultFactory(taxiResultsPage, taxiFareCalculator);
            var googleMapsDirectionsResponse = new GoogleMapsDirectionsResponse(webResponseReader);
            var googleMapsApiDeserialiser = new GoogleMapsApiDeserialiser();
            var specifyConditionsOfNoTaxiRoutesFound = new SpecifyConditionsOfNoTaxiRoutesFound();
            var hotelStore = new HotelCache();
            var downloadHtml = new DownloadHtml(webResponseReader);
            var retrieveElementText = new HtmlElement();
            var websiteScraper = new HotelScraper(hotelStore, downloadHtml, retrieveElementText);
            var hotelResultFactory = new HotelResultFactory(websiteScraper);
            var distanceCalculator = new DistanceCalculator(googleMapsDirectionsResponse, googleMapsApiDeserialiser, specifyConditionsOfNoTaxiRoutesFound);
            var whoIsTheWinner = new WhoIsTheWinner();
            _resultsViewModelFactory = new ResultsViewModelFactory(taxiResultFactory, hotelResultFactory, distanceCalculator, whoIsTheWinner);
            _createLocations = new LocationFactory();
        }

        public ViewResult Index(string from, string to, string fromlatlong, string tolatlong)
        {
            var resultsViewModel = new ResultsViewModel();
            try
            {
                var startingPoint = new StartingPoint(_createLocations.GetLocation(fromlatlong), from);
                var destination = new Destination(_createLocations.GetLocation(tolatlong), to);

                resultsViewModel = _resultsViewModelFactory.Create(Url, startingPoint, destination);

                return View("Index", resultsViewModel);
            }
            catch (Exception e)
            {
                resultsViewModel.Error = e;
            }

            return View("Error", resultsViewModel);
        }

        public ViewResult Fight()
        {
            return View();
        }
    }

    public interface ICreateLocations
    {
        Location GetLocation(string latlong);
    }

    public class LocationFactory : ICreateLocations
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