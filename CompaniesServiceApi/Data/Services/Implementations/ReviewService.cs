using CompaniesServiceApi.Data.Services.Interfaces;
using OfficeManager.Shared.Entities;

namespace CompaniesServiceApi.Data.Services.Implementations
{
    public class ReviewService : GenericService<Review>, IReviewService
    {
        public ReviewService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
