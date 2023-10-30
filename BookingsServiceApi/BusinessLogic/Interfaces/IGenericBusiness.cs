﻿namespace BookingsServiceApi.BusinessLogic.Interfaces
{
    public interface IGenericBusiness<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity?> Get(int id);
        Task Insert(TEntity entity);
        Task Update(TEntity entity);

        //Task Delete(TEntity entity);
        Task DeleteById(int id);

    }
}
