using CompaniesServiceApi.Data.Services.Interfaces;
using OfficeManager.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompaniesServiceApi.Data.Services.Implementations
{
    public class SeatService : GenericService<Seat>, ISeatService
    {
        public SeatService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Seat>> GetSeatsByRoomId(int roomId)
        {
            return await entities
                .Where(b => b.RoomId == roomId && b.IsDeleted != true).ToListAsync();
        }

        public async Task<bool> CheckSeatExist(int roomId, int coordinateX, int coordinateY)
        {
            return await entities
                .AnyAsync(b => b.RoomId == roomId && b.CoordinateX == coordinateX && b.CoordinateY == coordinateY);
        }

        public async Task<Seat> GetSeatByCoordinates(int roomId, int coordinateX, int coordinateY)
        {
            return await entities.SingleAsync(b => b.RoomId == roomId && b.CoordinateX == coordinateX && b.CoordinateY == coordinateY);
        }
    }
}
