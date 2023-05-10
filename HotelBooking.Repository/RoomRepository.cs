using System;
using HotelBooking.Entities.Domain;
using HotelBooking.Repository.Contracts;

namespace HotelBooking.Repository
{
	public class RoomRepository : IRoomRepository
	{
        private List<Room> _inMemoryRoom;

        public RoomRepository()
        {
            _inMemoryRoom = new List<Room>
            {
                new Room { RoomId = 1, StartReservation = DateTime.Parse("2023-06-01"), EndReservation = DateTime.Parse("2023-06-15")}
            };
        }

        public IEnumerable<Room> SeeReservations()
        {
            return _inMemoryRoom;
        }

        public Room GetReservationById(int id)
        {
            return _inMemoryRoom.Where(r => r.RoomId == id).FirstOrDefault();
        }

        public Room MakeReservation(Room newReservation)
        {
            var id = _inMemoryRoom.Select(r => r.RoomId).Max() + 1;
            newReservation.RoomId = id;
            _inMemoryRoom.Add(newReservation);
            return _inMemoryRoom.Where(r => r.RoomId == id).FirstOrDefault();
        }

        public Room UpdatePutReservation(int id)
        {
            return _inMemoryRoom.Where(r => r.RoomId == id).FirstOrDefault();
        }

        public Room UpdatePatchReservation(int id)
        {
            return _inMemoryRoom.Where(r => r.RoomId == id).FirstOrDefault();
        }

        public string CancelReservation(int id)
        {
            return "Cancel Reservation";
        }
    }
}

