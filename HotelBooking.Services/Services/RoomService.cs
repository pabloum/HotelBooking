using System;
using HotelBooking.Repository.Contracts;
using HotelBooking.Services.Services.Contracts;

namespace HotelBooking.Services.Services
{
	public class RoomService : IRoomService
    {
		private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
		{
			_roomRepository = roomRepository;
        }

		public string SeeReservations()
		{
			return _roomRepository.SeeReservations();
		}

        public string GetReservationById(int id)
		{
			return _roomRepository.GetReservationById(id);
		}

		public string MakeReservation(int id)
		{
			return _roomRepository.MakeReservation(id);
        }

		public string UpdatePutReservation(int id)
		{
			return _roomRepository.UpdatePutReservation(id);
        }

		public string UpdatePatchReservation(int id)
		{
			return _roomRepository.UpdatePatchReservation(id);
        }

		public string CancelReservation(int id)
		{
			return _roomRepository.CancelReservation(id);
        }
	}
}

