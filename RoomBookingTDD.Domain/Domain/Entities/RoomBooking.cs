using RoomBokingTDD.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBokingTDD.Domain.Entities
{
    public record RoomBooking:RoomBookingBase
    {
        public int? Id { get; set; }
        public int RoomBookingId { get; set; }
        public virtual Room Room { get; set; }
    }
}
