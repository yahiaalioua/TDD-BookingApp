namespace RoomBoking.Core.Tests
{
    public record RoomBookingRequest
    {

        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }
}