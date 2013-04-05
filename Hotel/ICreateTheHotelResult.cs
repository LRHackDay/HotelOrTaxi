using Geography;

namespace Results
{
    public interface ICreateTheHotelResult
    {
        HotelResult Create(StartingPoint startingPoint);
    }
}