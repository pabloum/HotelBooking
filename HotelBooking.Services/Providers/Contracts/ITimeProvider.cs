using System;
using HotelBooking.Services.Base;

namespace HotelBooking.Services.Providers.Contracts
{
	public interface IProvider
	{
	}

	public interface ITimeProvider : IProvider
	{
		DateTime GetCurrentDateTime();
	}
}

