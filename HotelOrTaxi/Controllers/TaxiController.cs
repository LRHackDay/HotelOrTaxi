using System.Web.Mvc;
using Geography;
using HotelOrTaxi.Models;
using TaxiFirmDetails;

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
            Latitude latitude1 = new Latitude(latitude);
            Longitude longitude1 = new Longitude(longitude);
            Location location = new Location(latitude1, longitude1);
            TaxisViewModel taxisViewModel = _taxiViewModelFactory.Create(location);
            return View("Index", taxisViewModel);
        }
    }
}