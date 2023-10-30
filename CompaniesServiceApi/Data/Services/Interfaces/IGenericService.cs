namespace CompaniesServiceApi.Data.Services.Interfaces
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity?> Get(int id);
        Task Insert(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity, bool IsToSaveChanges);
        Task DeleteById(int id);
    }
}