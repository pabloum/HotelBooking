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
using HotelBooking.Services.Helpers;

namespace HotelBooking.Tests.Services
{
	public class ReservationValidatorTest
	{
		private IReservationValidationService _reservationValidator;

		public ReservationValidatorTest()
		{
			var _mockRepository = new Mock<IRoomRepository>();
			_mockRepository.Setup(r => r.SeeReservations()).Returns(MockedDataFactory.GetMockedRooms());

			var timeProviderMock = new Mock<ITimeProvider>();
            timeProviderMock.Setup(t => t.GetCurrentDateTime()).Returns(new DateTime(2023,09,19));

            _reservationValidator = new ReservationValidator(_mockRepository.Object, timeProviderMock.Object);
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

			//Assert
			Assert.Equal(ex.Message, Constants.Error_Generic);
			Assert.Equal(ex.ValidationErrors.FirstOrDefault().Message, Constants.Error_UnavailableDates);
        }

        [Fact]
        public void TestEndDaterBeforeStartDate()
        {
            //Arrange
            var room = new Room
            {
                StartReservation = new DateTime(2023, 09, 11),
                EndReservation = new DateTime(2023, 09, 09),
            };

            //Act and Assert
            var ex = Assert.Throws<ValidationException.ValidationException>(() => _reservationValidator.IsReservationPossible(room));

			//Assert
			Assert.Equal(ex.Message, Constants.Error_Generic);
            Assert.Equal(ex.ValidationErrors.FirstOrDefault().Message, Constants.Error_NonLogicalEndDate);
        }

        [Fact]
        public void TestReservationLongerThan3Days()
        {
            //Arrange
            var room = new Room
            {
                StartReservation = new DateTime(2023, 09, 20),
                EndReservation = new DateTime(2023, 09, 25),
            };

            //Act and Assert
            var ex = Assert.Throws<ValidationException.ValidationException>(() => _reservationValidator.IsReservationPossible(room));

            //Assert
            Assert.Equal(ex.Message, Constants.Error_Generic);
            Assert.Equal(ex.ValidationErrors.FirstOrDefault().Message, Constants.Error_ReservationMoreThan3Days);
        }

        [Fact]
        public void TestReservation30DaysInAdvance()
        {
            //Arrange
            var room = new Room
            {
                StartReservation = new DateTime(2023, 10, 20),
                EndReservation = new DateTime(2023, 10, 22),
            };

            //Act and Assert
            var ex = Assert.Throws<ValidationException.ValidationException>(() => _reservationValidator.IsReservationPossible(room));

            //Assert
            Assert.Equal(ex.Message, Constants.Error_Generic);
            Assert.Equal(ex.ValidationErrors.FirstOrDefault().Message, Constants.Error_MoreThan30DaysInAdvance);
        }

        [Fact]
        public void TestIsReservatioForAtLeastNextDayOfBooking()
        {
            //Arrange
            var room = new Room
            {
                StartReservation = new DateTime(2023, 09, 19),
                EndReservation = new DateTime(2023, 09, 21),
            };

            //Act and Assert
            var ex = Assert.Throws<ValidationException.ValidationException>(() => _reservationValidator.IsReservationPossible(room));

            //Assert
            Assert.Equal(ex.Message, Constants.Error_Generic);
            Assert.Equal(ex.ValidationErrors.FirstOrDefault().Message, Constants.Error_ReservatioNotForAtLeastNextDayOfBooking);
        }
    }
}
    
