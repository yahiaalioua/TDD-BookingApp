
using RoomBokingTDD.Models.Enums;

namespace RoomBokingTDD.Domain.Models
{
    public record RoomBookingResult : RoomBookingBase
    {
        public BookingResultFlag Flag { get; set; }
        public int? RoomBookingId { get; set; }
    }
}