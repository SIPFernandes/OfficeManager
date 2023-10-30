namespace OfficeManagerApp.Areas.Services.Interfaces
{
    public interface IGenericHttpService<TEntity, TRequestModel, TResponseModel> 
        where TEntity : class where TResponseModel : class where TRequestModel : class
    {
        Task<IList<TEntity>> GetAll();
        Task Insert(TRequestModel entity);
        Task Update(TRequestModel entity, int Id);
        Task Delete(int Id);
        Task<TResponseModel> Get(int Id);
    }
}