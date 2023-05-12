using System;
using System.ComponentModel.DataAnnotations;
using HotelBooking.Entities.Domain;
using ValidationException = HotelBooking.Entities.Exceptions;
using HotelBooking.Repository.Contracts;
using HotelBooking.Services.Services;
using HotelBooking.Services.Services.Contracts;
using HotelBooking.Tests.DataFactory;
using Moq;
using HotelBooking.Services.Providers.Contracts;

namespace HotelBooking.Tests.Services
{
	public class ReservationValidatorTest
	{
		private IReservationValidationService _reservationValidator;
		private Mock<IRoomRepository> _mockRepository;

		public ReservationValidatorTest()
		{
			_mockRepository = new Mock<IRoomRepository>();
			_mockRepository.Setup(r => r.SeeReservations()).Returns(MockedDataFactory.GetMockedRooms());

			var timeProviderMock = new Mock<ITimeProvider>();
            timeProviderMock.Setup(t => t.GetCurrentDateTime()).Returns(new DateTime(2023,09,19));

            _reservationValidator = new ReservationValidationService(_mockRepository.Object, timeProviderMock.Object);
		}

		[Fact]
		public void TestValidReservation()
		{
			//Arrange
			var room = new Room
			{
				StartReservation = new DateTime(2023,09,20),
				EndReservation = new DateTime(2023,09,22),
            };

			//Act
			var result = _reservationValidator.IsReservationPossible(room);

			//Assert
			Assert.True(result);
        }

		[Fact]
		public void TestUnavailableDates()
		{
			//Arrange
			var room = new Room
			{
                StartReservation = new DateTime(2023, 09, 09),
                EndReservation = new DateTime(2023, 09, 11),
            };

			//Act and Assert
			var ex = Assert.Throws<ValidationException.ValidationException>(() => _reservationValidator.IsReservationPossible(room));
			Assert.Equal(ex.Message, "At least one validation error");
			Assert.Equal(ex.ValidationErrors.FirstOrDefault().Message, "The room is occupied in these dates");
        }
	}
}

