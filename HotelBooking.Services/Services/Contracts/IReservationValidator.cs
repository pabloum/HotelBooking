﻿using System;
using HotelBooking.Entities.Domain;
using HotelBooking.Services.Base;

namespace HotelBooking.Services.Services.Contracts
{
	public interface IReservationValidator : IService
    {
		bool IsReservationPossible(Room room);
	}
}