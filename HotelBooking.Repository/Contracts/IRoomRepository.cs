using System;
using HotelBooking.Entities.Domain;
using HotelBooking.Repository.Base;

namespace HotelBooking.Repository.Contracts
{
	public interface IRoomRepository : IRepository 
	{
        IEnumerable<Room> SeeReservations();
        Room GetReservationById(int id);
        Room MakeReservation(Room newReservation);
        Room UpdatePutReservation(int id);
        Room UpdatePatchReservation(int id);
        string CancelReservation(int id);
    }
}

