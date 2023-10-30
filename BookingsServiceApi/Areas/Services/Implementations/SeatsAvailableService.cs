using BookingsServiceApi.Areas.Services.Interfaces;
using BookingsServiceApi.Data;
using BookingsServiceApi.Data.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using OfficeManager.Shared.Entities;

namespace BookingsServiceApi.Areas.Services.Implementations
{
    public class SeatsAvailableService : GenericService<SeatsAvailable>, ISeatsAvailableService
    {
        public SeatsAvailableService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<SeatsAvailable> CheckBookingSeatExist(int roomId, DateTime date)
        {
            return await entities.FirstOrDefaultAsync(b => b.RoomId == roomId && b.Date.Date == date.Date && b.IsDeleted == false);
        }

        public async Task<IEnumerable<SeatsAvailable>> GetSeatsUnavailableByDate(DateTime date)
        {
            return await entities.Where(b => b.Date.Date == date.Date && b.AvailableSeatsNumber <= 0 && b.IsDeleted == false).ToListAsync();
        }

        public async Task<SeatsAvailable> GetSeatsLeftByRoomIdDate(int roomId, DateTime date)
        {
            return await entities.FirstOrDefaultAsync(b => b.Date.Date == date.Date && b.RoomId == roomId && b.IsDeleted == false);
        }
    }
}
