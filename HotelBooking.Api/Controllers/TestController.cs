using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        public TestController()
        {
        }

        [HttpGet("Ping")]
        public ActionResult<string> TestEndopoint() => Ok("Pong");
    }
}

