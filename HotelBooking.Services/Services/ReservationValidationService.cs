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
			if (!AreDatesAvailable())
			{
				return false;
			}

			if (!IsReservationWithLess30DaysInAdvance())
			{
				return false;
			}

			if (!IsReservationLessThan3Days())
			{
				return false;
			}

            if (!IsReservatioForAtLeastNextDayOfBooking())
            {
                return false;
            }

            return true;
		}

		private bool AreDatesAvailable()
		{
			return true;
		}

		private bool IsReservationWithLess30DaysInAdvance()
		{
			return true;
		}

		private bool IsReservationLessThan3Days()
        {
            return true;
        }

        private bool IsReservatioForAtLeastNextDayOfBooking()
        {
            return true;
        }
    }
}

