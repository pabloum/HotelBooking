using System;
using HotelBooking.Services.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("GetAllReservations")]
        public ActionResult<string> SeeReservations()
        {
            var result = _roomService.SeeReservations();
            return Ok(result);
        }

        [HttpGet("GetAllReservations/{id}")]
        public ActionResult<string> GetReservationById(int id)
        {
            var result = _roomService.GetReservationById(id);
            return Ok(result);
        }

        [HttpPost("BookRoom/{id}")]
        public ActionResult<string> MakeReservation(int id)
        {
            var result = _roomService.MakeReservation(id);
            return Ok(result);
        }

        [HttpPut("Update/{id}")]
        public ActionResult<string> UpdatePutReservation(int id)
        {
            var result = _roomService.UpdatePutReservation(id);
            return Ok(result);
        }

        [HttpPatch("UpdateAll/{id}")]
        public ActionResult<string> UpdatePatchReservation(int id)
        {
            var result = _roomService.UpdatePatchReservation(id);
            return Ok(result);
        }

        [HttpDelete("Cancel/{id}")]
        public ActionResult<string> CancelReservation(int id)
        {
            var result = _roomService.CancelReservation(id);
            return Ok(result);
        }
    }
}

