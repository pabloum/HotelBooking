using System;
using HotelBooking.Entities.Domain;
using HotelBooking.Entities.Exceptions;
using HotelBooking.Services.Services.Contracts;

namespace HotelBooking.Services.Services
{
	public class ReservationValidationService : IReservationValidationService
    {
		private readonly string UnavailableDates = "The room is occupied in these dates";
		private readonly string MoreThan30DaysInAdvance = "The reservations shouldn't be placed with more than 30 days in advance";
		private readonly string ReservationMoreThan3Days = "This reservation would take longer than 3 days";
		private readonly string ReservatioNotForAtLeastNextDayOfBooking = "Your reservations does not start either today or tomorrow";

        public ReservationValidationService()
		{
		}

		public bool IsReservationPossible(Room room)
		{
			var validationErrors = new List<string>();

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


        private bool AreDatesAvailable(DateTime startDate, DateTime endDate)
		{
			if (false)
			{
				return false;
            }

			return true;
		}

		private bool IsReservationWithLess30DaysInAdvance(DateTime startDate)
		{
            var currentDate = DateTime.Now;

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
            var currentDate = DateTime.Now;

            if (startDate > currentDate.AddDays(1))
			{
				return false;
			}

            return true;
        }
    }
}

