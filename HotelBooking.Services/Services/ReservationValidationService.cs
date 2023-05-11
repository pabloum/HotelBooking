using System;
using HotelBooking.Entities.Domain;
using HotelBooking.Services.Services.Contracts;

namespace HotelBooking.Services.Services
{
	public class ReservationValidationService : IReservationValidationService
    {
		public ReservationValidationService()
		{
		}

		public bool IsReservationPossible(Room room)
		{
			if (!AreDatesAvailable(room.StartReservation, room.EndReservation))
			{
				return false;
			}

			if (!IsReservationWithLess30DaysInAdvance(room.StartReservation))
			{
				return false;
			}

			if (!IsReservationLessThan3Days(room.StartReservation, room.EndReservation))
			{
				return false;
			}

            if (!IsReservatioForAtLeastNextDayOfBooking(room.StartReservation))
            {
                return false;
            }

            return true;
		}

		private bool AreDatesAvailable(DateTime startDate, DateTime endDate)
		{
			return true;
		}

		private bool IsReservationWithLess30DaysInAdvance(DateTime startDate)
		{
            var currentDate = DateTime.Now;

			if (currentDate.AddDays(30).Date > startDate.Date)
			{
				return false;
			}

            return true;
		}

		private bool IsReservationLessThan3Days(DateTime startDate, DateTime endDate)
        {
            if (endDate - startDate <= new TimeSpan(3,0,0,0))
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

