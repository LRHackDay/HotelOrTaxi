using System;
using System.Collections.Generic;

namespace LateRoomsScraper
{
    public class HotelScraperResponse : IScraperResponse
    {
        public List<Hotel> Hotels;
        public Guid ResultsGuid;
    }
}