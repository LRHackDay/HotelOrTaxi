using System.Web.Mvc;
using Geography;

namespace HotelOrTaxi.Controllers
{
    public class TaxiController : Controller
    {
        private readonly ICreateTaxiViewModels _taxiViewModelFactory;

        public TaxiController(ICreateTaxiViewModels taxiViewModelFactory)
        {
            _taxiViewModelFactory = taxiViewModelFactory;
        }

        public ViewResult Index(string latitude, string longitude)
        {
            var latitude1 = new Latitude(latitude);
            var longitude1 = new Longitude(longitude);
            var location = new Location(latitude1, longitude1);
            var taxisViewModel = _taxiViewModelFactory.Create(location);
            return View("Index", taxisViewModel);
        }
    }
}