using AutoMapper;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;

namespace CompaniesServiceApi.Mappers
{
    public class FacilityMapper : Profile
    {
        public FacilityMapper()
        {
            CreateMap<FacilityRequestModel, Facility>().ReverseMap();
        }
    }
}
