using System;
using HotelBooking.Entities.Domain;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Persistence.Context
{
	public class HotelBookingDbContext : DbContext, IDisposable
    {
		public HotelBookingDbContext(DbContextOptions<HotelBookingDbContext> options) : base(options)
		{
		}

		public virtual DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(r => r.RoomId);
                entity.ToTable("Room");
            });
        }
    }
}

