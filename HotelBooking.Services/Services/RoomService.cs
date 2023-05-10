using System;
using HotelBooking.Entities.Domain;
using HotelBooking.Entities.DTO;
using HotelBooking.Entities.Mappers;
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

		public IEnumerable<RoomDTO> SeeReservations()
		{
			return _roomRepository.SeeReservations().Select(r => r.MapToRoomDTO());
		}

        public RoomDTO GetReservationById(int id)
		{
			return _roomRepository.GetReservationById(id).MapToRoomDTO();
		}

		public RoomDTO MakeReservation(RoomDTO newReservationDto)
		{
			var newReservation = newReservationDto.MapToRoom();
            return _roomRepository.MakeReservation(newReservation).MapToRoomDTO();
        }

		public RoomDTO UpdatePutReservation(int id)
		{
			return _roomRepository.UpdatePutReservation(id).MapToRoomDTO();
        }

		public RoomDTO UpdatePatchReservation(int id)
		{
			return _roomRepository.UpdatePatchReservation(id).MapToRoomDTO();
        }

		public string CancelReservation(int id)
		{
			return _roomRepository.CancelReservation(id);
        }
	}
}

