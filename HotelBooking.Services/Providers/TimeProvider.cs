using System;
using HotelBooking.Services.Providers.Contracts;

namespace HotelBooking.Services.Providers
{
	public class TimeProvider : ITimeProvider
	{
		public DateTime GetCurrentDateTime()
		{
			return DateTime.Now;
		}
	}
}

