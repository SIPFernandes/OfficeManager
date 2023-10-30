using CompaniesServiceApi.BusinessLogic.Implementations;
using CompaniesServiceApi.Data.Services.Interfaces;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Exceptions;
using Microsoft.Extensions.Logging;
using Moq;

namespace OfficeManager.UnitTests.Business
{
    public class LocationBusinessTests
    {
        private ILogger<LocationBusiness> logger;

        [Fact]
        public async Task ValidateLocation_ExistentLocation_ShouldReturnTrue()
        {
            Mock<ILocationService> locationMock = new Mock<ILocationService>();

            LocationModel location = CreateLocation(1);

            locationMock.Setup(x => x.CheckLocationExist(location.Id, location.Country, location.City)).ReturnsAsync(false);

            LocationBusiness locationBusiness = new LocationBusiness(locationMock.Object, logger);

            var result = await locationBusiness.Validate(location);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ValidateLocation_NonExistentLocation_ShouldThrowEntityDuplicateException()
        {
            Mock<ILocationService> locationMock = new Mock<ILocationService>();

            LocationModel location = CreateLocation(1);

            locationMock.Setup(x => x.CheckLocationExist(location.Id, location.Country, location.City)).ReturnsAsync(true);

            LocationBusiness locationBusiness = new LocationBusiness(locationMock.Object, logger);

            // Assert
            await Assert.ThrowsAsync<EntityDuplicateException>(() => locationBusiness.Validate(location));
        }

        private LocationModel CreateLocation(int id)
        {
            return new LocationModel()
            {
                Id = id,
                Country = "Portugal",
                City = "Faro"
            };
        }
    }
}
