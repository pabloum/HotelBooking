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
			var validationErrors = new Dictionary<string, bool>();

			var isEndDateBeforeStartDate = EndDateIsBeforeStartDate(room.StartReservation, room.EndReservation);
			var areDatesUnvailable = !AreDatesAvailable(room);
			var isReservationMoreThan30days = IsReservationWithMore30DaysInAdvance(room.StartReservation);
			var isReservationLongerThan3Days = IsReservationLongerThan3Days(room.StartReservation, room.EndReservation);
			var isReservationNotForAtLeastNextDay = IsReservatioNotForAtLeastNextDayOfBooking(room.StartReservation);

			validationErrors.Add(Constants.Error_NonLogicalEndDate, isEndDateBeforeStartDate);
			validationErrors.Add(Constants.Error_UnavailableDates, areDatesUnvailable);
			validationErrors.Add(Constants.Error_MoreThan30DaysInAdvance, isReservationMoreThan30days);
			validationErrors.Add(Constants.Error_ReservationMoreThan3Days, isReservationLongerThan3Days);
			validationErrors.Add(Constants.Error_ReservatioNotForAtLeastNextDayOfBooking, isReservationNotForAtLeastNextDay);

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

