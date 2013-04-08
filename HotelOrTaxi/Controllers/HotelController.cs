﻿using System;
using System.Web.Mvc;
using LateRoomsScraper;

namespace HotelOrTaxi.Controllers
{
    public class HotelController : Controller
    {
        public ActionResult Index(Guid id)
        {
            var hotelStore = new HotelCache();
            var hotelResults = hotelStore.Get(id);

            return View(hotelResults);
        }
    }
}