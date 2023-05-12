using System;
using HotelBooking.Entities.Domain;
using HotelBooking.Entities.Mappers;
using HotelBooking.Entities.DTO;
using HotelBooking.Repository.Contracts;
using HotelBooking.Services.Providers.Contracts;
using HotelBooking.Services.Services;
using HotelBooking.Services.Services.Contracts;
using HotelBooking.Tests.DataFactory;
using Moq;

namespace HotelBooking.Tests.Services
{
	public class RoomServiceTest
	{
        private Mock<IRoomRepository> _mockRepository;
        private Mock<ITimeProvider> _timeProviderMock;
        private IRoomService _roomService;

		public RoomServiceTest()
		{
            _mockRepository = new Mock<IRoomRepository>();

            _timeProviderMock = new Mock<ITimeProvider>();
            _timeProviderMock.Setup(t => t.GetCurrentDateTime()).Returns(new DateTime(2023, 09, 19));
        }

        [Fact]
        public void TestGetAllReservations()
        {
            //Arrange
            _mockRepository.Setup(r => r.SeeReservations()).Returns(MockedDataFactory.GetMockedRooms());
            var reservationValidator = new ReservationValidationService(_mockRepository.Object, _timeProviderMock.Object);
            _roomService = new RoomService(_mockRepository.Object, reservationValidator);

            //Act
            var results = _roomService.SeeReservations();

            //Assert
            Assert.NotNull(results);
            Assert.Equal(results.Count(), 3);
        }

        [Fact]
        public void TestGetSingleReservationById()
        {
            //Arrange
            _mockRepository.Setup(r => r.GetReservationById(It.IsAny<int>())).Returns(MockedDataFactory.GetMockedRooms().FirstOrDefault());
            var reservationValidator = new ReservationValidationService(_mockRepository.Object, _timeProviderMock.Object);
            _roomService = new RoomService(_mockRepository.Object, reservationValidator);

            //Act
            var result = _roomService.GetReservationById(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.StartReservation, new DateTime(2023, 08 ,09));
            Assert.Equal(result.EndReservation, new DateTime(2023, 08 ,19));
        }

        [Fact]
        public void TestCreateNewReservation()
        {
            //Arrange
            var newRoom = new Room { StartReservation = new DateTime(2023, 09, 20), EndReservation = new DateTime(2023, 09, 21) };
            _mockRepository.Setup(r => r.MakeReservation(It.IsAny<Room>())).Returns(MockedDataFactory.GetMockedRooms().FirstOrDefault());
            var reservationValidator = new ReservationValidationService(_mockRepository.Object, _timeProviderMock.Object);
            _roomService = new RoomService(_mockRepository.Object, reservationValidator);

            //Act
            var result = _roomService.MakeReservation(newRoom.MapToRoomDTO());

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.StartReservation, new DateTime(2023, 08, 09));
            Assert.Equal(result.EndReservation, new DateTime(2023, 08, 19));
        }


        [Fact]
        public void TestUpdateReservation()
        {
            //Arrange
            var newRoom = new Room { StartReservation = new DateTime(2023, 09, 20), EndReservation = new DateTime(2023, 09, 21) };
            _mockRepository.Setup(r => r.UpdatePutReservation(It.IsAny<int>(), It.IsAny<Room>())).Returns(MockedDataFactory.GetMockedRooms().FirstOrDefault());
            var reservationValidator = new ReservationValidationService(_mockRepository.Object, _timeProviderMock.Object);
            _roomService = new RoomService(_mockRepository.Object, reservationValidator);

            //Act
            var result = _roomService.UpdatePutReservation(1, newRoom.MapToRoomDTO());

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.StartReservation, new DateTime(2023, 08, 09));
            Assert.Equal(result.EndReservation, new DateTime(2023, 08, 19));
        }

        [Fact]
        public void TestCancelReservation()
        {
            //Arrange
            _mockRepository.Setup(r => r.CancelReservation(It.IsAny<int>())).Returns("Reservation canceled");
            var reservationValidator = new ReservationValidationService(_mockRepository.Object, _timeProviderMock.Object);
            _roomService = new RoomService(_mockRepository.Object, reservationValidator);

            //Act
            var result = _roomService.CancelReservation(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result, "Reservation canceled");
        }
    }
}

