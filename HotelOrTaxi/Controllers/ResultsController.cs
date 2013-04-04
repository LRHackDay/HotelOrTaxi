using System.Web.Mvc;
using HotelOrTaxi.Models;
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
            var hotelResultFactory = new HotelResultFactory();

            _resultsViewModelFactory = new ResultsViewModelFactory(taxiResultFactory, hotelResultFactory);
        }

        public ViewResult Index(string from, string to, string fromlatlong, string tolatlong)
        {
            ICreateResultViewModels resultsViewModelFactory = _resultsViewModelFactory;
            ResultsViewModel resultsViewModel = resultsViewModelFactory.Create(Url);

            return View("Index", resultsViewModel);
        }
    }
}