using System;
using HotelBooking.Repository.Base;

namespace HotelBooking.Repository.Contracts
{
	public interface IRoomRepository : IRepository 
	{
        string SeeReservations();
        string GetReservationById(int id);
        string MakeReservation(int id);
        string UpdatePutReservation(int id);
        string UpdatePatchReservation(int id);
        string CancelReservation(int id);
    }
}

