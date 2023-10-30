using AutoMapper;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;

namespace CompaniesServiceApi.Mappers
{
    public class SeatMapper : Profile
    {
        public SeatMapper()
        {
            CreateMap<SeatRequestModel, Seat>().ReverseMap();
        }
    }
}
