using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using LateRoomsScraper;

namespace HotelOrTaxi.Controllers
{
    public class HotelController : Controller
    {

        public ActionResult Index(Guid id)
        {
            var hotelResults = HttpRuntime.Cache.Get(id.ToString()) as List<Hotel>;

            if (hotelResults == null)
            {
                hotelResults = new List<Hotel>();   
            }

            return View(hotelResults);
        }

    }
}
