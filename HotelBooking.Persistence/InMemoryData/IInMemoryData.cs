using HotelBooking.Entities.Domain;

namespace HotelBooking.Persistence.InMemoryData
{
    public interface IInMemoryData
    {
        IEnumerable<Room> GetAll();
        Room GetById(int id);
        Room Add(Room room);
        Room Update(int id, Room room);
        void Remove(int id);
    }
}

