using System;
using HotelBooking.Entities.DTO;
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
        public ActionResult<IEnumerable<RoomDTO>> SeeReservations()
        {
            var result = _roomService.SeeReservations();
            return Ok(result);
        }

        [HttpGet("GetMyReservation/{id}")]
        public ActionResult<RoomDTO> GetReservationById(int id)
        {
            var result = _roomService.GetReservationById(id);
            return Ok(result);
        }

        [HttpPost("BookRoom")]
        public ActionResult<RoomDTO> MakeReservation([FromBody]RoomDTO roomDTO)
        {
            var result = _roomService.MakeReservation(roomDTO);
            return Ok(result);
        }

        [HttpPut("Update/{id}")]
        public ActionResult<RoomDTO> UpdatePutReservation(int id, [FromBody] RoomDTO roomDTO)
        {
            var result = _roomService.UpdatePutReservation(id, roomDTO);
            return Ok(result);
        }

        [HttpPatch("UpdateAll/{id}")]
        public ActionResult<RoomDTO> UpdatePatchReservation(int id, [FromBody] RoomDTO roomDTO)
        {
            var result = _roomService.UpdatePatchReservation(id, roomDTO);
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

