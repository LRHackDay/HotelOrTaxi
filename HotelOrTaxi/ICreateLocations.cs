using Geography;

namespace HotelOrTaxi
{
    public interface ICreateLocations
    {
        Location GetLocation(string latlong);
    }
}