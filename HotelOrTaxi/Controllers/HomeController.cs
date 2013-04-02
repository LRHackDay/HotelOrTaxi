using System.Web.Mvc;

namespace HotelOrTaxi.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View("Index");
        }
    }
}
