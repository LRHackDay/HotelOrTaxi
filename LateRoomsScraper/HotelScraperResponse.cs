using System.Collections.Generic;

namespace LateRoomsScraper
{
    public class HotelScraperResponse : IScraperResponse
    {
        public Hotel Hotel;
        public List<Hotel> Hotels;
    }
}