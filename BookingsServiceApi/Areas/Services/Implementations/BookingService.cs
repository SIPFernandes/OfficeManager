using Microsoft.EntityFrameworkCore;
using OfficeManager.Shared.Entities;
using BookingsServiceApi.Data.Services.Interfaces;

namespace BookingsServiceApi.Data.Services.Implementations
{
    public class BookingService : GenericService<Booking>, IBookingService
    {
        public BookingService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> CheckBookingExist(int id, int userId, int roomId, DateTime date)
        {
            return await entities
                .AnyAsync(b => b.UserId == userId && b.Date.Date == date.Date && b.Id != id && b.IsDeleted == false && b.RoomId != roomId);
        }

        public async Task<IEnumerable<Booking>> GetBookingsByDateRoomId(int roomId, DateTime date)
        {
            return await entities
                .Where(x => x.RoomId == roomId && x.Date.Date == date.Date && x.IsDeleted != true)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookingsByDate(DateTime date)
        {
            return await entities
                .Where(x => x.Date.Date == date.Date && x.IsDeleted != true)
                .ToListAsync();
        }
    }
}
