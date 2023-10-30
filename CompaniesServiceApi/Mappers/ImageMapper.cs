using AutoMapper;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;

namespace CompaniesServiceApi.Mappers
{
    public class ImageMapper : Profile
    {
        public ImageMapper()
        {
            CreateMap<ImageRequestModel, Image>().ReverseMap();
        }
    }
}
