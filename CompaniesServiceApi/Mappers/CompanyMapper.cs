using AutoMapper;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;

namespace CompaniesServiceApi.Mappers
{
    public class CompanyMapper : Profile
    {
        public CompanyMapper()
        {
            CreateMap<CompanyRequestModel, Company>()
              .ForMember(x => x.Image,
                         y => y.MapFrom(z => string.IsNullOrEmpty(z.Image) ? null : new Image { File = z.Image }))
              .ReverseMap();
        }
    }
}