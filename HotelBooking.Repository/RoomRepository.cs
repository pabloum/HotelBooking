using HotelBooking.Entities.Domain;
using HotelBooking.Repository.Contracts;
using HotelBooking.Persistence.InMemoryData;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Repository
{
    public class RoomRepository : IRoomRepository
	{
        private readonly DbContext _context;
        private readonly bool _useDataBase;
        private IInMemoryData _inMemoryData;

        private DbSet<Room> DbSet => _context.Set<Room>();

        public RoomRepository(DbContext context, IInMemoryData inMemoryData)
        {
            _context = context;
            _inMemoryData = inMemoryData;
            _useDataBase = true;
        }

        public IEnumerable<Room> SeeReservations()
        {
            return _useDataBase ? DbSet : _inMemoryData.GetAll();
        }

        public Room GetReservationById(int id)
        {
            return _useDataBase ? DbSet.Where(r => r.RoomId == id).FirstOrDefault() : _inMemoryData.GetById(id);
        }

        public Room MakeReservation(Room newReservation)
        {
            return _inMemoryData.Add(newReservation);
        }

        public Room UpdatePutReservation(int id, Room updatedReservation)
        {
            return _inMemoryData.Update(id, updatedReservation);
        }

        public string CancelReservation(int id)
        {
            _inMemoryData.Remove(id);
            return "Reservation canceled";
        }
    }
}

