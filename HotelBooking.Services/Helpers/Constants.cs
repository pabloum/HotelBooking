using System;
namespace HotelBooking.Services.Helpers
{
	public static class Constants
	{
        public static readonly string Error_Generic = "At least one validation error occured";
        public static readonly string Error_NonLogicalEndDate = "The end date should be after the start date";
        public static readonly string Error_UnavailableDates = "The room is occupied in these dates";
        public static readonly string Error_MoreThan30DaysInAdvance = "The reservations shouldn't be placed with more than 30 days in advance";
        public static readonly string Error_ReservationMoreThan3Days = "This reservation would take longer than 3 days";
        public static readonly string Error_ReservatioNotForAtLeastNextDayOfBooking = "Your reservations does not start at least tomorrow";
    }
}

