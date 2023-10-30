using AutoMapper;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;
using OfficeManager.Shared.Response_Model;

namespace CompaniesServiceApi.Mappers
{
    public class RoomMapper : Profile
    {
        public RoomMapper()
        {
            CreateMap<RoomRequestModel, Room>().ReverseMap();
            
            CreateMap<RoomResponseModel, Room>().ReverseMap();

            CreateMap<RoomResponseModel, RoomRequestModel>().ReverseMap();
        }
    }
}
