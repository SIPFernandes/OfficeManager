using CompaniesServiceApi.Data;
using CompaniesServiceApi.Data.Services.Implementations;
using OfficeManager.Shared.Entities;
using OfficeManager.UnitTests.Mocks;

namespace OfficeManager.UnitTests.Services
{
    public class CompanyServiceTests
    {
        public readonly MockDbContextFactoryCompanies mockDbContextFactory;

        private ApplicationDbContext context;

        public CompanyServiceTests()
        {
            mockDbContextFactory = new MockDbContextFactoryCompanies();

            context = mockDbContextFactory.CreateDbContext();

        }

        private CompanyService CreateCompanyService()
        {
            RoomService roomService = new RoomService(context);

            OfficeService officeService = new OfficeService(roomService, context);

            return new CompanyService(officeService, context);
        }

        [Fact]
        public async Task UpdateCompany_ShouldReturnEntity()
        {
            CompanyService companyService = CreateCompanyService();

            await companyService.Insert(CreateCompany(1));

            var company = await companyService.Get(1);

            company.Description = "Some text";

            await companyService.Update(company);

            // Assert
            Assert.Equal("Metyis", company.Name);
            Assert.Equal("Some text", company.Description);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task CheckCompanyExist_ExistentCompany_ShouldReturnTrue()
        {
            CompanyService companyService = CreateCompanyService();

            await companyService.Insert(CreateCompany(1));

            var company = CreateCompany(2);

            var result = await companyService.CheckCompanyExist(company.Name, company.Id);

            // Assert
            Assert.True(result);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task CheckCompanyExist_NonExistentCompany_ShouldReturnFalse()
        {
            CompanyService companyService = CreateCompanyService();

            await companyService.Insert(CreateCompany(1));

            var company = CreateCompany(2);

            company.Name = "Apple";

            var result = await companyService.CheckCompanyExist(company.Name, company.Id);

            // Assert
            Assert.False(result);

            context.Database.EnsureDeleted();
        }

        private Company CreateCompany(int id)
        {
            return new Company()
            {
                Id = id,
                Name = "Metyis",
                Description = "description",
                Image = new Image()
                {
                    File = "image"
                }
            };
        }
    }
}
