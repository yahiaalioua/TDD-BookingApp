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
            var response=_context.Rooms.Where(x=> !x.RoomBooking.Any(q=>q.Date == date)).ToList();
           
            return response;
            
        }

        public void Save(RoomBooking roomBooking)
        {
            _context.RoomBookings.Add(roomBooking);
            _context.SaveChanges();
        }
    }
}
