using System;
using HotelBooking.Entities.Domain;

namespace HotelBooking.Repository.InMemoryData
{
    public class InMemoryData : IInMemoryData
    {
        private List<Room> _inMemoryRoom;

        public InMemoryData()
        {
            _inMemoryRoom = new List<Room>
            {
                new Room { RoomId = 1, StartReservation = DateTime.Parse("2023-06-01"), EndReservation = DateTime.Parse("2023-06-15")}
            };
        }
        public IEnumerable<Room> GetAll()
        {
            return _inMemoryRoom;
        }

        public Room GetById(int id)
        {
            return _inMemoryRoom.Where(r => r.RoomId == id).FirstOrDefault();
        }

        public Room Add(Room newReservation)
        {
            var id = _inMemoryRoom.Select(r => r.RoomId).Max() + 1;
            newReservation.RoomId = id;
            _inMemoryRoom.Add(newReservation);
            return _inMemoryRoom.Where(r => r.RoomId == id).FirstOrDefault();
        }

        public Room Update(int id, Room room)
        {
            var index = _inMemoryRoom.FindIndex(r => r.RoomId == id);
            _inMemoryRoom[index] = room;
            return room;
        }

        public void Remove(int id)
        {
            _inMemoryRoom.RemoveAll(r => r.RoomId == id);
        }
    }

}

