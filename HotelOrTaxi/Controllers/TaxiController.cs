using System.Web.Mvc;
using HotelOrTaxi.Models;

namespace HotelOrTaxi.Controllers
{
    public class TaxiController : Controller
    {
        public ViewResult Index()
        {
            return View("Index", new TaxisViewModel());
        }
    }
}