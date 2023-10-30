using OfficeManager.Shared.Entities;

namespace CompaniesServiceApi.Data.Services.Interfaces
{
    public interface ILocationService : IGenericService<LocationModel>
    {
        Task<bool> CheckLocationExist(int id, string country, string city);
    }
}
