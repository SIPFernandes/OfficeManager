using Microsoft.EntityFrameworkCore;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Exceptions;
using BookingsServiceApi.Data.Services.Interfaces;

namespace BookingsServiceApi.Data.Services.Implementations
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _dbContext;
        protected DbSet<TEntity> entities;

        public GenericService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            entities = _dbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await entities
                .Where(x => x.IsDeleted != true)
                .ToListAsync();
        }

        public virtual async Task<TEntity?> Get(int id)
        {
            var entity = await entities.FindAsync(id);

            if (entity != null && !entity.IsDeleted)
            {
                return entity;
            }
            else
            {
                throw new EntityDoesNotExistException();
            }

        }

        public async Task Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Add(entity);

            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Update(entity);

            _dbContext
                .Entry(entity)
                .Property("Id").IsModified = false;

            _dbContext
                .Entry(entity)
                .Property("CreatedAt").IsModified = false;

            _dbContext
                .Entry(entity)
                .Property("ModifiedAt").IsModified = false;

            _dbContext
                .Entry(entity)
                .Property("IsDeleted").IsModified = false;

            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entity.IsDeleted = true;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<TEntity> DeleteById(int id)
        {
            var entity = await Get(id);

            await Delete(entity);

            return entity;
        }
    }
}