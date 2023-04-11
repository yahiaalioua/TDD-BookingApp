using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoomBokingTDD.Core.Services.Abstract;
using RoomBokingTDD.Domain.Models;
using RoomBokingTDD.Models.Enums;

namespace RoomBookingTDD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomBookingController : ControllerBase
    {
        private readonly IRoomBookingProcessor _roomBookingProcessor;

        public RoomBookingController(IRoomBookingProcessor roomBookingProcessor)
        {
            _roomBookingProcessor = roomBookingProcessor;
        }

        public async Task<IActionResult> Book(RoomBookingRequest request)
        {
            if(ModelState.IsValid)
            {
                var result = _roomBookingProcessor.Book(request);

                if (result.Flag == BookingResultFlag.Success)
                {
                    return Ok(result);
                };
                ModelState.AddModelError(nameof(result.Date), "No rooms available for given date");
            }
            return BadRequest(ModelState);
        }
    }
}
