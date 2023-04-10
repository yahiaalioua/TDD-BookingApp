﻿namespace RoomBokingTDD.Domain.Entities
{
    public record Room
    {
        public int RoomId { get; set; }
        public string Name { get; set; } = null!;
        public virtual RoomBooking RoomBooking { get; set; }
    }
}