using RoomBokingTDD.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBokingTDD.Core.Domain.Entities
{
    public record RoomBooking:RoomBookingBase
    {
        public int RoomBookingId { get; set; }
        public int? Id { get; set; }
    }
}
