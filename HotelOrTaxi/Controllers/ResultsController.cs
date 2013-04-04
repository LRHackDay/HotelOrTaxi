using System.Web.Mvc;
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
            var latitude = fromlatlong.Split(',')[0];
            var longitude = fromlatlong.Split(',')[1];
            var resultsViewModel = resultsViewModelFactory.Create(Url, latitude, longitude);

            return View("Index", resultsViewModel);
        }
    }
}