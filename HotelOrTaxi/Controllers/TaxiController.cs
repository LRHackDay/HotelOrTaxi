using System.Web.Mvc;
using Geography;
using HotelOrTaxi.Models;
using TaxiFirmDetails;
using WebResponse;

namespace HotelOrTaxi.Controllers
{
    public class TaxiController : Controller
    {
        private readonly ICreateTaxiViewModels _taxiViewModelFactory;

        public TaxiController(ICreateTaxiViewModels taxiViewModelFactory)
        {
            _taxiViewModelFactory = taxiViewModelFactory;
        }

        public TaxiController()
        {
            var webClientWrapper = new WebClientWrapper();
            var configReader = new ConfigReader();
            var taxiFirmFactory = new TaxiFirmFactory(new GoogleTextSearchRequestConstructor(configReader),
                                                      new GooglePlaceRequestConstructor(configReader),
                                                      webClientWrapper);
            _taxiViewModelFactory = new TaxiViewModelFactory(taxiFirmFactory);
        }

        public ViewResult Index()
        {
            var latitude = new Latitude("53.479251");
            var longitude = new Longitude("-2.247926");
            var location = new Location(latitude, longitude);
            TaxisViewModel taxisViewModel = _taxiViewModelFactory.Create(location);
            return View("Index", taxisViewModel);
        }
    }
}