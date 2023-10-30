using AutoMapper;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;

namespace CompaniesServiceApi.Mappers
{
    public class RoomFacilityMapper : Profile
    {
        public RoomFacilityMapper()
        {
            CreateMap<RoomFacilityRequestModel, RoomFacility>().ReverseMap();
        }
    }
}
