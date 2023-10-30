using AutoMapper;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;

namespace CompaniesServiceApi.Mappers
{
    public class LocationMapper : Profile
    {
        public LocationMapper()
        {
            CreateMap<LocationRequestModel, LocationModel>().ReverseMap();
        }
    }
}
