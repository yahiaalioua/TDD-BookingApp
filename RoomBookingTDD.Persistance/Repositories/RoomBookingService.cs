using RoomBokingTDD.Core.Services;
using RoomBokingTDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBookingTDD.Persistance.Repositories
{
    public class RoomBookingService : IRoomBookingInterface
    {
        private RoomBookingDbContext _context;

        public RoomBookingService(RoomBookingDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Room> GetAvailableRooms(DateTime date)
        {
            var allBookings=_context.RoomBookings.ToList();
            var relevantBookingsToDate=allBookings.FindAll(r=>r.Date == date).ToList();
            var allRooms=_context.Rooms.ToList();
            var availableRooms=new List<Room>();
            foreach(var r in relevantBookingsToDate)
            {
                var results=allRooms.FindAll(x => x.RoomId != r.Id);
                availableRooms= results.ToList();
            }
            return availableRooms;
            
        }

        public void Save(RoomBooking roomBooking)
        {
            throw new NotImplementedException();
        }
    }
}
