using CompaniesServiceApi.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using OfficeManager.Shared.Entities;

namespace CompaniesServiceApi.Data.Services.Implementations
{
    public class RoomService : GenericService<Room>, IRoomService
    {
        public RoomService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Room>> GetRoomsByOfficeId(int officeId)
        {
            return await entities
                .Where(x => x.OfficeId == officeId && x.IsDeleted != true)
                .Include(x => x.Images)
                .ToListAsync();
        }

        public async Task<IEnumerable<Room>> GetAvailableRooms(IList<int> unavailableRoomsId)
        {
            return await entities
                .Where(x => !unavailableRoomsId.Contains(x.Id) && x.IsDeleted != true && x.CanBook != false)
                .Include(x => x.Office)
                .ThenInclude(x => x.Location)
                .Include(x => x.Office)
                .ThenInclude(x => x.Image)
                .Include(x => x.Seats)
                .ToListAsync();
        }

        public async Task<IEnumerable<Room>> GetAvailableRoomsByOfficeId(int officeId, IList<int> unavailableRoomsId)
        {
            return await entities
                .Where(x => x.OfficeId == officeId && x.IsDeleted != true && x.CanBook != false && !unavailableRoomsId.Contains(x.Id))
                .Include(x => x.Images)
                .ToListAsync();
        }

        public async Task<bool> CheckRoomExist(int id, string name, int officeId)
        {
            return await entities
                .AnyAsync(b => b.Name == name && b.OfficeId == officeId && b.Id != id && b.IsDeleted == false);
        }

        public override async Task<Room?> Get(int id)
        {
            var room = await base.Get(id);

            return await entities.Include(x => x.Images)
                .Include(x => x.Seats)
                .Include(x => x.RoomFacilities)
                .ThenInclude(x => x.Facility)
                .ThenInclude(x => x.Image)
                .SingleAsync(x => x.Id == id);
        }

        public override async Task<IEnumerable<Room>> GetAll()
        {
            return await entities
                .Where(x => x.IsDeleted != true)
                .Include(x => x.Images)
                .Include(x => x.Seats)
                .Include(x => x.Office)
                .Include(x => x.RoomFacilities)
                .ThenInclude(x => x.Facility)
                .ThenInclude(x => x.Image)
                .ToListAsync();
        }

        public override async Task Update(Room room)
        {
            if (room == null)
            {
                throw new ArgumentNullException("entity");
            }

            var roomEntity = _dbContext.Entry(room);

            //TODO: Try to improve.
            if(_dbContext.RoomFacility != null && !_dbContext.RoomFacility.Any()) 
            {
                var facilitiesToRemove = _dbContext.RoomFacility.Where(x => x.RoomId == room.Id && !room.RoomFacilities.Select(y => y.Id).Contains(x.Id));

                _dbContext.RoomFacility.RemoveRange(facilitiesToRemove);
            }

            var seatsToRemove = _dbContext.Seat.Where(x => x.RoomId == room.Id && !room.Seats.Contains(x));

            var imagesToRemove = _dbContext.Image.Where(x => x.RoomId == room.Id && !room.Images.Contains(x));

            _dbContext.Seat.RemoveRange(seatsToRemove);

            _dbContext.Image.RemoveRange(imagesToRemove);

            BeforeInsert(room);

            entities.Update(room);
                        
            roomEntity.Property("Id")
                .IsModified = false;

            roomEntity.Property("CreatedAt")
                .IsModified = false;

            roomEntity.Property("ModifiedAt")
                .IsModified = false;

            roomEntity.Property("IsDeleted")
                .IsModified = false;

            await _dbContext.SaveChangesAsync();
        }

        public override void BeforeInsert(Room entity)
        {
            if (entity.Seats != null && entity.Seats.Any())
            {
                entity.CanBook = true;
            }
        }

        public async Task DeleteByOfficeId(int officeId)
        {
            await entities
                .Where(x => x.IsDeleted != true && x.OfficeId == officeId)
                .ForEachAsync(x => x.IsDeleted = true);
        }

        public async Task<IDictionary<int, Room>> GetRoomsByIds(IList<int> roomIds)
        {
            return await entities
                .Where(x => x.IsDeleted != true && roomIds.Contains(x.Id))
                .Include(x => x.Images)
                .Include(x => x.Office)
                .ThenInclude(x => x.Location)
                .ToDictionaryAsync(x => x.Id, x => x);
        }
    }
}