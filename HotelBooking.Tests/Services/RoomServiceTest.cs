using System;
using HotelBooking.Repository.Contracts;
using HotelBooking.Services.Services;
using HotelBooking.Services.Services.Contracts;
using Moq;

namespace HotelBooking.Tests.Services
{
	public class RoomServiceTest
	{
		private IRoomService _roomService;
		private Mock<IRoomRepository> _mockRepository;

		public RoomServiceTest()
		{
			_mockRepository = new Mock<IRoomRepository>();
            _roomService = new RoomService(_mockRepository.Object, new ReservationValidationService());
		}

		[Fact]
		public void Test()
		{
			//Arrange
			//Act
			//Assert
		}
	}
}

