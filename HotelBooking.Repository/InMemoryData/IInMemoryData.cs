using HotelBooking.Entities.Domain;

namespace HotelBooking.Repository.InMemoryData
{
    public interface IInMemoryData
    {
        IEnumerable<Room> GetAll();
        Room GetById(int id);
        Room Add(Room room);
        void Remove(int id);
    }
}

