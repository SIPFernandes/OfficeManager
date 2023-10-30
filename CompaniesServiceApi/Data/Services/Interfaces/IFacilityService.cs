using OfficeManager.Shared.Entities;

namespace CompaniesServiceApi.Data.Services.Interfaces
{
    public interface IFacilityService : IGenericService<Facility>
    {
        Task<bool> CheckFacilityExist(string name, int id);
    }
}
