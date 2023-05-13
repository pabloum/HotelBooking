using HotelBooking.Entities.Domain;
using HotelBooking.Repository.Contracts;
using HotelBooking.Persistence.InMemoryData;
using Microsoft.EntityFrameworkCore;
using HotelBooking.Persistence.Context;
using Microsoft.Extensions.Configuration;

namespace HotelBooking.Repository
{
    public class RoomRepository : IRoomRepository
	{
        private readonly HotelBookingDbContext _context;
        private readonly bool _useDataBase;
        private IInMemoryData _inMemoryData;

        private DbSet<Room> DbSet => _context.Set<Room>();

        public RoomRepository(HotelBookingDbContext context, IInMemoryData inMemoryData, IConfiguration configuration)
        {
            _context = context;
            _inMemoryData = inMemoryData;

            _useDataBase = configuration.GetSection("UseDataBase").Value == "True";
        }

        public IEnumerable<Room> SeeReservations()
        {

            return _useDataBase ? DbSet.AsNoTracking() : _inMemoryData.GetAll();
        }

        public Room GetReservationById(int id)
        {
            return _useDataBase ? DbSet.Where(r => r.RoomId == id).AsNoTracking().FirstOrDefault() : _inMemoryData.GetById(id);
        }

        public Room MakeReservation(Room newReservation)
        {
            if (_useDataBase)
            {
                var added = DbSet.Add(newReservation);
                _context.SaveChanges();
                return added.Entity;
            }
            else
            {
                return _inMemoryData.Add(newReservation);
            }
        }

        public Room UpdatePutReservation(int id, Room updatedReservation)
        {
            if (_useDataBase)
            {
                var added = DbSet.Update(updatedReservation);
                _context.SaveChanges();
                return added.Entity;
            }
            else
            {
                return _inMemoryData.Update(id, updatedReservation);
            }
        }

        public string CancelReservation(int id)
        {
            if (_useDataBase)
            {
                DbSet.Remove(GetReservationById(id));
                _context.SaveChanges();
            }
            else
            {
                _inMemoryData.Remove(id);
            }

            return "Reservation canceled";
        }
    }
}

