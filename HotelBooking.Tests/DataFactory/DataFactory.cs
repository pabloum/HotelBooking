using System;
using HotelBooking.Entities.Domain;

namespace HotelBooking.Tests.DataFactory
{
    public static class MockedDataFactory
	{
		private static IEnumerable<Room> _rooms = new List<Room>
		{
			new Room{ RoomId = 1, StartReservation = DateTime.Parse("2023-08-09"), EndReservation = DateTime.Parse("2023-08-19")},
			new Room{ RoomId = 2, StartReservation = DateTime.Parse("2023-09-09"), EndReservation = DateTime.Parse("2023-09-18")},
			new Room{ RoomId = 3, StartReservation = DateTime.Parse("2023-10-09"), EndReservation = DateTime.Parse("2023-10-19")},
        };

		public static IEnumerable<Room> GetMockedRooms()
		{
			return _rooms;
		}
	}
}

