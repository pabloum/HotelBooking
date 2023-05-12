using System;
using HotelBooking.Entities.Domain;
using HotelBooking.Entities.Exceptions;
using HotelBooking.Repository.Contracts;
using HotelBooking.Services.Providers.Contracts;
using HotelBooking.Services.Services.Contracts;

namespace HotelBooking.Services.Services
{
	public class ReservationValidationService : IReservationValidationService
    {
		private readonly string NonLogicalEndDate = "The end date should be after the start date";
		private readonly string UnavailableDates = "The room is occupied in these dates";
		private readonly string MoreThan30DaysInAdvance = "The reservations shouldn't be placed with more than 30 days in advance";
		private readonly string ReservationMoreThan3Days = "This reservation would take longer than 3 days";
		private readonly string ReservatioNotForAtLeastNextDayOfBooking = "Your reservations does not start either today or tomorrow";

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
				validationErrors.Add(NonLogicalEndDate);
			}

			if (!AreDatesAvailable(room.StartReservation, room.EndReservation))
			{
				validationErrors.Add(UnavailableDates);
			}

			if (!IsReservationWithLess30DaysInAdvance(room.StartReservation))
			{
				validationErrors.Add(MoreThan30DaysInAdvance);
            }

			if (!IsReservationLessThan3Days(room.StartReservation, room.EndReservation))
			{
				validationErrors.Add(ReservationMoreThan3Days);
            }

            if (!IsReservatioForAtLeastNextDayOfBooking(room.StartReservation))
            {
				validationErrors.Add(ReservatioNotForAtLeastNextDayOfBooking);
            }

			CheckValidationsAndThrowExceptions(validationErrors);

            return true;
		}

		private void CheckValidationsAndThrowExceptions(IEnumerable<string> validationErrors)
		{
			if (validationErrors.Any())
			{
				throw new ValidationException("At least one validation error", validationErrors);
			}
		}

        private bool EndDateIsBeforeStartDate(DateTime startDate, DateTime endDate)
		{
			return endDate.Date <= startDate.Date;
		}

        private bool AreDatesAvailable(DateTime startDate, DateTime endDate)
		{
			var allReservations = _roomRepository.SeeReservations();

			foreach (var reservation in allReservations)
			{
                if (reservation.StartReservation.Date < endDate.Date && startDate.Date < reservation.EndReservation.Date)
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

            if (startDate > currentDate.AddDays(1))
			{
				return false;
			}

            return true;
        }
    }
}

