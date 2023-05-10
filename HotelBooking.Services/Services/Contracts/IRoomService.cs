using System;
using HotelBooking.Entities.Domain;
using HotelBooking.Entities.DTO;
using HotelBooking.Services.Base;

namespace HotelBooking.Services.Services.Contracts
{
	public interface IRoomService : IService
	{
        IEnumerable<RoomDTO> SeeReservations();
        RoomDTO GetReservationById(int id);
        RoomDTO MakeReservation(RoomDTO newReservationDTO);
        RoomDTO UpdatePutReservation(int id);
        RoomDTO UpdatePatchReservation(int id);
        string CancelReservation(int id);
    }
}

