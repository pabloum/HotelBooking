﻿using System;
using HotelBooking.Repository.Contracts;

namespace HotelBooking.Repository
{
	public class RoomRepository : IRoomRepository
	{
        public string SeeReservations()
        {
            return "See Room";
        }

        public string GetReservationById(int id)
        {
            return "Get Reservation";
        }

        public string MakeReservation(int id)
        {
            return "Make Reservation";
        }

        public string UpdatePutReservation(int id)
        {
            return "Update put reservation";
        }

        public string UpdatePatchReservation(int id)
        {
            return "Update patch Reservation";
        }

        public string CancelReservation(int id)
        {
            return "Cancel Reservation";
        }
    }
}

