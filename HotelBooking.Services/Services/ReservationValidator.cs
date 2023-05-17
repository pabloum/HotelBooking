using System;
using HotelBooking.Entities.Domain;
using HotelBooking.Entities.Exceptions;
using HotelBooking.Repository.Contracts;
using HotelBooking.Services.Helpers;
using HotelBooking.Services.Providers.Contracts;
using HotelBooking.Services.Services.Contracts;

namespace HotelBooking.Services.Services
{
	public class ReservationValidator : IReservationValidator
    {
		private readonly ITimeProvider _timeProvider;
		private readonly IRoomRepository _roomRepository;

        public ReservationValidator(IRoomRepository roomRepository, ITimeProvider timeProvider)
		{
			_roomRepository = roomRepository;
			_timeProvider = timeProvider;
        }

		public bool IsReservationPossible(Room room)
		{
			var validationErrors = new Dictionary<string, bool>()
			{
				{ Constants.Error_NonLogicalEndDate, EndDateIsBeforeStartDate(room.StartReservation, room.EndReservation) },
				{ Constants.Error_UnavailableDates, !AreDatesAvailable(room) },
				{ Constants.Error_MoreThan30DaysInAdvance, IsReservationWithMore30DaysInAdvance(room.StartReservation) },
				{ Constants.Error_ReservationMoreThan3Days, IsReservationLongerThan3Days(room.StartReservation, room.EndReservation) },
				{ Constants.Error_ReservatioNotForAtLeastNextDayOfBooking, IsReservatioNotForAtLeastNextDayOfBooking(room.StartReservation) },
			};


			var errors = validationErrors.Where(x => x.Value).Select(x => x.Key);

            if (errors.Any())
            {
                throw new ValidationException(Constants.Error_Generic, errors);
            }

            return true;
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
					(reservation.StartReservation.Date <= room.EndReservation.Date
					&& room.StartReservation.Date <= reservation.EndReservation.Date);

                if (areDatesoverlaping && room.RoomId != reservation.RoomId)
				{
					return false;
				}
			}

			return true;
		}

		private bool IsReservationWithMore30DaysInAdvance(DateTime startDate)
		{
            var currentDate = _timeProvider.GetCurrentDateTime();
			return currentDate.AddDays(30).Date <= startDate.Date;
		}

		private bool IsReservationLongerThan3Days(DateTime startDate, DateTime endDate)
        {
			return endDate - startDate > new TimeSpan(3, 0, 0, 0);
        }

        private bool IsReservatioNotForAtLeastNextDayOfBooking(DateTime startDate)
        {
            var currentDate = _timeProvider.GetCurrentDateTime();
			return startDate.Date < currentDate.AddDays(1).Date;
        }
    }
}

