using System;
using HotelBooking.Entities.Domain;
using HotelBooking.Repository.Contracts;
using HotelBooking.Persistence.InMemoryData;

namespace HotelBooking.Repository
{
    public class RoomRepository : IRoomRepository
	{
        private IInMemoryData _inMemoryData;

        public RoomRepository(IInMemoryData inMemoryData)
        {
            _inMemoryData = inMemoryData;
        }

        public IEnumerable<Room> SeeReservations()
        {
            return _inMemoryData.GetAll();
        }

        public Room GetReservationById(int id)
        {
            return _inMemoryData.GetById(id);
        }

        public Room MakeReservation(Room newReservation)
        {
            return _inMemoryData.Add(newReservation);
        }

        public Room UpdatePutReservation(int id, Room updatedReservation)
        {
            return _inMemoryData.Update(id, updatedReservation);
        }

        public string CancelReservation(int id)
        {
            _inMemoryData.Remove(id);
            return "Reservation canceled";
        }
    }
}

