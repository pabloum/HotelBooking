using System;
using HotelBooking.Services.Base;

namespace HotelBooking.Services.Services.Contracts
{
	public interface IRoomService : IService
	{
        string SeeReservations();
        string GetReservationById(int id);
        string MakeReservation(int id);
        string UpdatePutReservation(int id);
        string UpdatePatchReservation(int id);
        string CancelReservation(int id);
    }
}

