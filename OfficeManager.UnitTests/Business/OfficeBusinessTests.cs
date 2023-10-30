using CompaniesServiceApi.BusinessLogic.Implementations;
using CompaniesServiceApi.Data.Services.Interfaces;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Exceptions;
using Microsoft.Extensions.Logging;
using Moq;

namespace OfficeManager.UnitTests.Business
{
    public class OfficeBusinessTests
    {
        private ILogger<OfficeBusiness> logger;

        [Fact]
        public async Task ValidateOffice_ExistentOffice_ShouldReturnTrue()
        {
            Mock<IOfficeService> officeMock = new Mock<IOfficeService>();

            Office office = CreateOffice(1);

            officeMock.Setup(x => x.CheckOfficeExist(office.Id, office.Name, office.CompanyId)).ReturnsAsync(false);

            OfficeBusiness officeBusiness = new OfficeBusiness(officeMock.Object, logger);

            var result = await officeBusiness.Validate(office);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ValidateOffice_NonExistentOffice_ShouldThrowEntityDuplicateException()
        {
            Mock<IOfficeService> officeMock = new Mock<IOfficeService>();

            Office office = CreateOffice(1);

            officeMock.Setup(x => x.CheckOfficeExist(office.Id, office.Name, office.CompanyId)).ReturnsAsync(true);

            OfficeBusiness officeBusiness = new OfficeBusiness(officeMock.Object, logger);

            // Assert
            await Assert.ThrowsAsync<EntityDuplicateException>(() => officeBusiness.Validate(office));
        }

        private Office CreateOffice(int id)
        {
            return new Office()
            {
                Id = id,
                Name = "Faro Office",
                Image = new Image()
                {
                    File = "image"
                },
                CompanyId = 1,
                Company = new Company()
                {
                    Id = 1,
                    Name = "Metyis",
                    Description = "description",
                    Image = new Image()
                    {
                        File = "image"
                    }
                },
                Location = new LocationModel()
                {
                    Country = "Portugal",
                    City = "Faro"
                }
            };
        }
    }
}
