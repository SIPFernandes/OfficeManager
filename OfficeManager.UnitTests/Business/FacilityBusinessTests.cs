using CompaniesServiceApi.BusinessLogic.Implementations;
using CompaniesServiceApi.Data.Services.Interfaces;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Exceptions;
using Microsoft.Extensions.Logging;
using Moq;

namespace OfficeManager.UnitTests.Business
{
    public class FacilityBusinessTests
    {
        private ILogger<FacilityBusiness> logger;

        [Fact]
        public async Task ValidateFacility_ExistentFacility_ShouldReturnTrue()
        {
            Mock<IFacilityService> facilityMock = new Mock<IFacilityService>();

            Facility facility = CreateFacility(1);

            facilityMock.Setup(x => x.CheckFacilityExist(facility.Name, facility.Id)).ReturnsAsync(false);

            FacilityBusiness facilityBusiness = new FacilityBusiness(facilityMock.Object, logger);

            var result = await facilityBusiness.Validate(facility);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ValidateFacility_NonExistentFacility_ShouldThrowEntityDuplicateException()
        {
            Mock<IFacilityService> facilityMock = new Mock<IFacilityService>();

            Facility facility = CreateFacility(1);

            facilityMock.Setup(x => x.CheckFacilityExist(facility.Name, facility.Id)).ReturnsAsync(true);

            FacilityBusiness facilityBusiness = new FacilityBusiness(facilityMock.Object, logger);

            // Assert
            await Assert.ThrowsAsync<EntityDuplicateException>(() => facilityBusiness.Validate(facility));
        }

        private Facility CreateFacility(int id)
        {
            return new Facility()
            {
                Id = id,
                Name = "WiFi",
                Image = new Image()
                {
                    File = "image"
                }
            };
        }
    }
}
