using System.Web.Mvc;
using Geography;
using HotelOrTaxi.Models;
using LateRoomsScraper;
using Results;

namespace HotelOrTaxi.Controllers
{
    public class ResultsController : Controller
    {
        private readonly ICreateResultViewModels _resultsViewModelFactory;

        public ResultsController(ICreateResultViewModels resultsViewModelFactory)
        {
            _resultsViewModelFactory = resultsViewModelFactory;
        }

        public ResultsController()
        {
            ICreateTheTaxiControllerUri taxiResultsPage = new CreateTheTaxiControllerUri();
            var taxiResultFactory = new TaxiResultFactory(taxiResultsPage);
            var hotelResultFactory = new HotelResultFactory(new HotelScraper());

            _resultsViewModelFactory = new ResultsViewModelFactory(taxiResultFactory, hotelResultFactory);
        }

        public ViewResult Index(string from, string to, string fromlatlong, string tolatlong)
        {
            ICreateResultViewModels resultsViewModelFactory = _resultsViewModelFactory;
            var startingPoint = new StartingPoint(fromlatlong);
            var destination = new Destination(tolatlong);
            var journey = new Journey(startingPoint, destination, new DistanceFactory());
            var resultsViewModel = resultsViewModelFactory.Create(Url, journey);

            return View("Index", resultsViewModel);
        }
    }
}