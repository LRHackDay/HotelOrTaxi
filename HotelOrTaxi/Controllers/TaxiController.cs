using System.Web.Mvc;

namespace HotelOrTaxi.Controllers
{
    public class TaxiController : Controller
    {
        private readonly ICreateTaxiViewModels _taxiViewModelFactory;

        public TaxiController()
        {
            _taxiViewModelFactory = new TaxiViewModelFactory();
        }

        public TaxiController(ICreateTaxiViewModels taxiViewModelFactory)
        {
            _taxiViewModelFactory = taxiViewModelFactory;
        }

        public ViewResult Index()
        {
            return View("Index", _taxiViewModelFactory.Create());
        }
    }
}