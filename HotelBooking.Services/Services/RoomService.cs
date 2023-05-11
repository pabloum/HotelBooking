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
		private readonly IReservationValidationService _reservationValidationService;

        public RoomService(IRoomRepository roomRepository, IReservationValidationService reservationValidationService)
		{
			_roomRepository = roomRepository;
			_reservationValidationService = reservationValidationService;
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

			if (_reservationValidationService.IsReservationPossible(newReservation))
			{
				return _roomRepository.MakeReservation(newReservation).MapToRoomDTO();
			}
			else
			{
				throw new Exception("Reservation was not possible");
			}
        }

		public RoomDTO UpdatePutReservation(int id, RoomDTO updatedReservationDto)
		{
            var newReservation = updatedReservationDto.MapToRoom();

            if (_reservationValidationService.IsReservationPossible(newReservation))
            {
				return _roomRepository.UpdatePutReservation(id).MapToRoomDTO();
            }
            else
            {
                throw new Exception("Reservation was not possible. Your original reservation is active");
            }
        }

		public RoomDTO UpdatePatchReservation(int id, RoomDTO updatedReservationDto)
		{
            var newReservation = updatedReservationDto.MapToRoom();

            if (_reservationValidationService.IsReservationPossible(newReservation))
            {
				return _roomRepository.UpdatePatchReservation(id).MapToRoomDTO();
            }
            else
            {
                throw new Exception("Reservation was not possible. Your original reservation is active");
            }
        }

		public string CancelReservation(int id)
		{
			return _roomRepository.CancelReservation(id);
        }
	}
}

