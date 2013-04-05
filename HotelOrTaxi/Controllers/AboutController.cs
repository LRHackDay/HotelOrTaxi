using System.Web.Mvc;

namespace HotelOrTaxi.Controllers
{
    public class AboutController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}