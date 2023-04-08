namespace RoomBokingTDD.Core.Models
{
    public abstract record RoomBookingBase
    {

        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }
}