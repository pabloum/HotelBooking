using System;
using HotelBooking.Entities.Domain;
using HotelBooking.Entities.DTO;

namespace HotelBooking.Entities.Mappers
{
	public static class RoomMappers
	{
		public static Room MapToRoom(this RoomDTO roomDTO)
		{
			return roomDTO == null ? null : new Room
			{
				RoomId = roomDTO.RoomId,
				StartReservation = roomDTO.StartReservation,
				EndReservation = roomDTO.EndReservation
            };
		}

        public static RoomDTO MapToRoomDTO(this Room room)
        {
			return room == null ? null : new RoomDTO
			{
				RoomId = room.RoomId,
                StartReservation = room.StartReservation,
                EndReservation = room.EndReservation
            };
        }
    }
}

