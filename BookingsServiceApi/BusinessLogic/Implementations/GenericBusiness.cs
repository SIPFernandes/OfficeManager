using BookingsServiceApi.BusinessLogic.Interfaces;
using BookingsServiceApi.Data.Services.Interfaces;

namespace BookingsServiceApi.BusinessLogic.Implementations
{
    public class GenericBusiness<TService, TEntity> : IGenericBusiness<TEntity> where TService : IGenericService<TEntity> where TEntity : class
    {
        protected readonly TService Service;

        protected readonly ILogger Logger;

        public GenericBusiness(TService service, ILogger logger)
        {
            Service = service;

            Logger = logger;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Service.GetAll();
        }

        public virtual async Task<TEntity?> Get(int id)
        {
            return await Service.Get(id);
        }              

        public virtual async Task Insert(TEntity entity)
        {
            if (await Validate(entity))
            {
                await Service.Insert(entity);
            }
        }

        public virtual async Task Update(TEntity entity)
        {
            if (await Validate(entity))
            {
                await Service.Update(entity);
            }
        }

        //public async Task Delete(TEntity entity) //not used for now
        //{
        //    await Service.Delete(entity);
        //}

        public virtual async Task<bool> Validate(TEntity entity)
        {
            return true;
        }

        public virtual async Task DeleteById(int id)
        {
            await Service.DeleteById(id);
        }

    }
}
