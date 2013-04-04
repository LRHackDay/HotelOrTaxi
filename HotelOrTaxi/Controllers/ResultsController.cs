using System.Web.Mvc;
using HotelOrTaxi.Models;

namespace HotelOrTaxi.Controllers
{
    


    public class ResultsController : Controller
    {
        private readonly ICreateTheTaxiControllerUri _createTheTaxiControllerUri;
        private readonly ResultsViewModelFactory _resultsViewModelFactory;

        public ResultsController(ICreateTheTaxiControllerUri createTheTaxiControllerUri)
        {
            _createTheTaxiControllerUri = createTheTaxiControllerUri;
            _resultsViewModelFactory = new ResultsViewModelFactory(_createTheTaxiControllerUri);
        }

        public ViewResult Index(string from, string to, string fromlatlong, string tolatlong)
        {
            var resultsViewModelFactory = _resultsViewModelFactory;
            var resultsViewModel = resultsViewModelFactory.Create(Url);

            return View("Index", resultsViewModel);
        }
    }

    

    
}