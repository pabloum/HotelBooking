using System;
using HotelBooking.Entities.Domain;
using HotelBooking.Entities.DTO;
using HotelBooking.Entities.Exceptions;
using HotelBooking.Entities.Mappers;
using HotelBooking.Repository.Contracts;
using HotelBooking.Services.Services.Contracts;

namespace HotelBooking.Services.Services
{
	public class RoomService : IRoomService
    {
		private readonly IRoomRepository _roomRepository;
		private readonly IReservationValidator _reservationValidationService;

        public RoomService(IRoomRepository roomRepository, IReservationValidator reservationValidationService)
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
			_reservationValidationService.IsReservationPossible(newReservation);
			return _roomRepository.MakeReservation(newReservation).MapToRoomDTO();
			
        }

		public RoomDTO UpdatePutReservation(int id, RoomDTO updatedReservationDto)
		{
			if (id != updatedReservationDto.RoomId)
			{
				throw new ValidationException("Id in url and body don't match");
			}

            var newReservation = updatedReservationDto.MapToRoom();
            _reservationValidationService.IsReservationPossible(newReservation);
            return _roomRepository.UpdatePutReservation(id, newReservation).MapToRoomDTO();
        }

		public string CancelReservation(int id)
		{
			return _roomRepository.CancelReservation(id);
        }
	}
}

