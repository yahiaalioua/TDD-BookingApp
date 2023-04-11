using Microsoft.EntityFrameworkCore;
using RoomBokingTDD.Domain.Entities;
using RoomBookingTDD.Persistance;
using RoomBookingTDD.Persistance.Repositories;

namespace RoomBookingTDD.Persistance.Tests
{
    public class RoomBookingPersistanceTests
    {
        [Fact]
        public void ShouldReturnAvailableRooms()
        {
            //arrange
            var date = new DateTime(2023, 04, 11);

            var options = new DbContextOptionsBuilder<RoomBookingDbContext>()
                .UseInMemoryDatabase(nameof(ShouldReturnAvailableRooms)).Options;
            
            var context= new RoomBookingDbContext(options);
            context.Add(new Room() { RoomId = 1, Name = "room 1" });
            context.Add(new Room() { RoomId = 2, Name = "room 2" });
            context.Add(new Room() { RoomId = 3, Name = "room 3" });

            context.Add(new RoomBooking() { Id = 1, Date = date });
            context.Add(new RoomBooking() { Id = 2, Date = date.AddDays(-1) });
            context.SaveChanges();

            var roomBookingService = new RoomBookingService(context);

            //act

            var availableRooms=roomBookingService.GetAvailableRooms(date);

            //assert

            Assert.Equal(2,availableRooms.Count());
            Assert.Contains(availableRooms, r=>r.RoomId == 2);
            Assert.Contains(availableRooms, r => r.RoomId == 3);
            Assert.DoesNotContain(availableRooms, r => r.RoomId == 1);

        }

        [Fact]
        public void ShouldSaveRoomBookingToDb()
        {
            //arrange

            var date = new DateTime(2023, 04, 11);
            var options = new DbContextOptionsBuilder<RoomBookingDbContext>()
                .UseInMemoryDatabase(nameof(ShouldSaveRoomBookingToDb)).Options;

            var context = new RoomBookingDbContext(options);
            var newBooking = new RoomBooking() { Id = 1, Date = date };

            //act
            var roomBookingService = new RoomBookingService(context);
            roomBookingService.Save(newBooking);

            var allBookings = context.RoomBookings.ToList();

            //assert
            var booking=Assert.Single(allBookings);

            Assert.Equal(newBooking.Date, booking.Date);
            Assert.Equal(newBooking.RoomBookingId, booking.RoomBookingId);

        }
    }
}