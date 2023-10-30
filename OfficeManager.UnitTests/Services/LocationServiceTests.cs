using CompaniesServiceApi.Data;
using CompaniesServiceApi.Data.Services.Implementations;
using OfficeManager.Shared.Entities;
using OfficeManager.UnitTests.Mocks;

namespace OfficeManager.UnitTests.Services
{
    public class LocationServiceTests
    {
        public readonly MockDbContextFactoryCompanies mockDbContextFactory;

        private ApplicationDbContext context;

        public LocationServiceTests()
        {
            mockDbContextFactory = new MockDbContextFactoryCompanies();

            context = mockDbContextFactory.CreateDbContext();

        }              

        [Fact]
        public async Task UpdateLocation_ShouldReturnEntity()
        {
            LocationService locationService = new LocationService(context);

            await locationService.Insert(CreateLocation(1));

            var location = await locationService.Get(1);

            location.City = "Lisbon";

            await locationService.Update(location);

            // Assert
            Assert.Equal("Portugal", location.Country);
            Assert.Equal("Lisbon", location.City);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task CheckLocationExist_ExistentLocation_ShouldReturnTrue()
        {
            LocationService locationService = new LocationService(context);

            await locationService.Insert(CreateLocation(1));

            var location = CreateLocation(2);

            var result = await locationService.CheckLocationExist(location.Id, location.Country, location.City);

            // Assert
            Assert.True(result);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task CheckLocationExist_NonExistentLocation_ShouldReturnFalse()
        {
            LocationService locationService = new LocationService(context);

            await locationService.Insert(CreateLocation(1));

            var location = CreateLocation(2);

            location.City = "Lisbon";

            var result = await locationService.CheckLocationExist(location.Id, location.Country, location.City);

            // Assert
            Assert.False(result);

            context.Database.EnsureDeleted();
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
