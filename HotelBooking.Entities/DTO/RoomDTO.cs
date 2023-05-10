using System;
namespace HotelBooking.Entities.DTO
{
	public class RoomDTO
	{
		public int RoomId { get; set; }
        public DateTime StartReservation { get; set; }
        public DateTime EndReservation { get; set; }
    }
}

