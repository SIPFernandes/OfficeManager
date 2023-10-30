using CompaniesServiceApi.BusinessLogic.Interfaces;
using CompaniesServiceApi.Data.Services.Interfaces;
using OfficeManager.Shared.Entities;

namespace CompaniesServiceApi.BusinessLogic.Implementations
{
    public class ImageBusiness : GenericBusiness<IImageService, Image>, IImageBusiness
    {
        public ImageBusiness(IImageService service, ILogger<ImageBusiness> logger) : base(service, logger)
        {
        }
    }
}
