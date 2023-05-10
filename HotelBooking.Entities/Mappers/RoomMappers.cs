using System;
using HotelBooking.Entities.Domain;
using HotelBooking.Entities.DTO;

namespace HotelBooking.Entities.Mappers
{
	public static class RoomMappers
	{
		public static Room MapToRoom(this RoomDTO roomDTO)
		{
			return new Room
			{
				RoomId = roomDTO.RoomId
			};
		}

        public static RoomDTO MapToRoomDTO(this Room room)
        {
			return new RoomDTO
			{
				RoomId = room.RoomId
			};
        }
    }
}

