using Microsoft.EntityFrameworkCore;
using RoomBokingTDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBookingTDD.Persistance
{
    public class RoomBookingDbContext : DbContext
    {
        public RoomBookingDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomBooking> RoomBookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().HasMany(x => x.RoomBooking)
                .WithOne(x => x.Room).HasForeignKey(r => r.Id);
            modelBuilder.Entity<Room>().HasData
                (
                    new Room() { RoomId=1,Name="Conference room A"},
                    new Room() { RoomId = 2, Name = "Conference room B" },
                    new Room() { RoomId = 3, Name = "Conference room C" }
                );
        }
    }
}
