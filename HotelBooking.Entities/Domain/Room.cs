using System;
namespace HotelBooking.Entities.Domain
{
	public class Room
	{
		public int RoomId { get; set; }
		public DateTime StartReservation { get; set; }
		public DateTime EndReservation { get; set; }
    }
}

