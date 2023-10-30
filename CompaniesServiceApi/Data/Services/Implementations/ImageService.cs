using CompaniesServiceApi.Data.Services.Interfaces;
using OfficeManager.Shared.Entities;

namespace CompaniesServiceApi.Data.Services.Implementations
{
    public class ImageService : GenericService<Image>, IImageService
    {
        public ImageService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
