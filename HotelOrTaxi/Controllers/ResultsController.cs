using System;
using System.Web.Mvc;
using Geography;
using TaxiApi.Configuration;
using TaxiApi.Request;
using TaxiApi.Response;
using WebResponse;

namespace HotelOrTaxi.Controllers
{
    public class ResultsController : Controller
    {
        public ActionResult Index(string from, string to, string fromlatlong, string tolatlong)
        {
            return View("Index");
        }
    }
}