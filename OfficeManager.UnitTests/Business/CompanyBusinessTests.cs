using CompaniesServiceApi.BusinessLogic.Implementations;
using CompaniesServiceApi.Data.Services.Interfaces;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Exceptions;
using Microsoft.Extensions.Logging;
using Moq;

namespace OfficeManager.UnitTests.Business
{
    public class CompanyBusinessTests
    {
        private ILogger<CompanyBusiness> logger;

        [Fact]
        public async Task ValidateCompany_ExistentCompany_ShouldReturnTrue()
        {
            Mock<ICompanyService> companyMock = new Mock<ICompanyService>();

            Company company = CreateCompany(1);

            companyMock.Setup(x => x.CheckCompanyExist(company.Name, company.Id)).ReturnsAsync(false);

            CompanyBusiness companyBusiness = new CompanyBusiness(companyMock.Object, logger);

            var result = await companyBusiness.Validate(company);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ValidateCompany_NonExistentCompany_ShouldThrowEntityDuplicateException()
        {
            Mock<ICompanyService> companyMock = new Mock<ICompanyService>();

            Company company = CreateCompany(1);

            companyMock.Setup(x => x.CheckCompanyExist(company.Name, company.Id)).ReturnsAsync(true);

            CompanyBusiness companyBusiness = new CompanyBusiness(companyMock.Object, logger);

            // Assert
            await Assert.ThrowsAsync<EntityDuplicateException>(() => companyBusiness.Validate(company));
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
