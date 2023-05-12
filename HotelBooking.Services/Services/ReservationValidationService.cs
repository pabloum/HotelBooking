using System;
using HotelBooking.Entities.Domain;
using HotelBooking.Entities.Exceptions;
using HotelBooking.Repository.Contracts;
using HotelBooking.Services.Helpers;
using HotelBooking.Services.Providers.Contracts;
using HotelBooking.Services.Services.Contracts;

namespace HotelBooking.Services.Services
{
	public class ReservationValidationService : IReservationValidationService
    {
		private readonly ITimeProvider _timeProvider;
		private readonly IRoomRepository _roomRepository;

        public ReservationValidationService(IRoomRepository roomRepository, ITimeProvider timeProvider)
		{
			_roomRepository = roomRepository;
			_timeProvider = timeProvider;
        }

		public bool IsReservationPossible(Room room)
		{
			var validationErrors = new List<string>();

			if (EndDateIsBeforeStartDate(room.StartReservation, room.EndReservation))
			{
				validationErrors.Add(Constants.Error_NonLogicalEndDate);
			}

			if (!AreDatesAvailable(room))
			{
				validationErrors.Add(Constants.Error_UnavailableDates);
			}

			if (!IsReservationWithLess30DaysInAdvance(room.StartReservation))
			{
				validationErrors.Add(Constants.Error_MoreThan30DaysInAdvance);
            }

			if (!IsReservationLessThan3Days(room.StartReservation, room.EndReservation))
			{
				validationErrors.Add(Constants.Error_ReservationMoreThan3Days);
            }

            if (!IsReservatioForAtLeastNextDayOfBooking(room.StartReservation))
            {
				validationErrors.Add(Constants.Error_ReservatioNotForAtLeastNextDayOfBooking);
            }

			CheckValidationsAndThrowExceptions(validationErrors);

            return true;
		}

		private void CheckValidationsAndThrowExceptions(IEnumerable<string> validationErrors)
		{
			if (validationErrors.Any())
			{
					throw new ValidationException(Constants.Error_Generic, validationErrors);
			}
		}

        private bool EndDateIsBeforeStartDate(DateTime startDate, DateTime endDate)
		{
			return endDate.Date <= startDate.Date;
		}

        private bool AreDatesAvailable(Room room)
		{
			var allReservations = _roomRepository.SeeReservations();

			foreach (var reservation in allReservations)
			{
				var areDatesoverlaping =
					(reservation.StartReservation.Date < room.EndReservation.Date
					&& room.StartReservation.Date < reservation.EndReservation.Date);

                if (areDatesoverlaping && room.RoomId != reservation.RoomId)
				{
					return false; 
				}
			}

			return true;
		}

		private bool IsReservationWithLess30DaysInAdvance(DateTime startDate)
		{
            var currentDate = _timeProvider.GetCurrentDateTime();

			if (currentDate.AddDays(30).Date < startDate.Date)
			{
				return false;
            }

            return true;
		}

		private bool IsReservationLessThan3Days(DateTime startDate, DateTime endDate)
        {
            if (endDate - startDate > new TimeSpan(3,0,0,0))
            {
				return false;
            }

            return true;
        }

        private bool IsReservatioForAtLeastNextDayOfBooking(DateTime startDate)
        {
            var currentDate = _timeProvider.GetCurrentDateTime();

            if (startDate.Date < currentDate.AddDays(1).Date)
			{
				return false;
			}

            return true;
        }
    }
}

