using RoomBokingTDD.Core.Models.Enums;

namespace RoomBokingTDD.Core.Models
{
    public record RoomBookingResult : RoomBookingBase
    {
        public BookingResultFlag Flag { get; set; }
        public int? RoomBookingId { get; set; }
    }
}