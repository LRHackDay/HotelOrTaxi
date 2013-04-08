using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;

namespace LateRoomsScraper
{
    public interface ISaveHotels
    {
        Guid Add(List<Hotel> hotels);
        List<Hotel> Get(Guid id);
    }

    public class HotelCache : ISaveHotels
    {
        public Guid Add(List<Hotel> hotels)
        {
            var resultsGuid = Guid.NewGuid();
            var key = resultsGuid.ToString();

            if (HttpRuntime.Cache.Get(key) == null)
            {
                HttpRuntime.Cache.Add(key, hotels, null, DateTime.Now.AddDays(1), TimeSpan.Zero,
                                      CacheItemPriority.Normal, null);
            }

            return resultsGuid;
        }

        public List<Hotel> Get(Guid id)
        {
            return HttpRuntime.Cache.Get(id.ToString()) as List<Hotel> ?? new List<Hotel>();
        }
    }
}