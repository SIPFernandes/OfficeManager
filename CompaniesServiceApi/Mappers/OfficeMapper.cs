using AutoMapper;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;

namespace CompaniesServiceApi.Mappers
{
    public class OfficeMapper : Profile
    {
        public OfficeMapper()
        {
            CreateMap<OfficeRequestModel, Office>()
              .ForMember(x => x.Image,
                         y => y.MapFrom(z => string.IsNullOrEmpty(z.Image) ? null : new Image { File = z.Image }))
              .ReverseMap();
        }
    }
}