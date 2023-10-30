using CompaniesServiceApi.BusinessLogic.Interfaces;
using CompaniesServiceApi.Data.Services.Interfaces;
using OfficeManager.Shared.Entities;

namespace CompaniesServiceApi.BusinessLogic.Implementations
{
    public class ReviewBusiness : GenericBusiness<IReviewService, Review>, IReviewBusiness
    {
        public ReviewBusiness(IReviewService service, ILogger<ReviewBusiness> logger) : base(service, logger)
        {
        }
    }
}
