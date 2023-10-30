using CompaniesServiceApi.Data;
using CompaniesServiceApi.Data.Services.Implementations;
using OfficeManager.Shared.Entities;
using OfficeManager.UnitTests.Mocks;

namespace OfficeManager.UnitTests.Services
{
    public class FacilityServiceTests
    {
        public readonly MockDbContextFactoryCompanies mockDbContextFactory;

        private ApplicationDbContext context;

        public FacilityServiceTests()
        {
            mockDbContextFactory = new MockDbContextFactoryCompanies();

            context = mockDbContextFactory.CreateDbContext();

        }

        [Fact]
        public async Task UpdateFacility_ShouldReturnEntity()
        {
            FacilityService facilityService = new FacilityService(context);

            await facilityService.Insert(CreateFacility(1));

            var facility = await facilityService.Get(1);

            facility.Image.File = "Some image";

            await facilityService.Update(facility);

            // Assert
            Assert.Equal("WiFi", facility.Name);
            Assert.Equal("Some image", facility.Image.File);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task CheckFacilityExist_ExistentFacility_ShouldReturnTrue()
        {
            FacilityService facilityService = new FacilityService(context);

            await facilityService.Insert(CreateFacility(1));

            var facility = CreateFacility(2);

            var result = await facilityService.CheckFacilityExist(facility.Name, facility.Id);

            // Assert
            Assert.True(result);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task CheckFacilityExist_NonExistentFacility_ShouldReturnFalse()
        {
            FacilityService facilityService = new FacilityService(context);

            await facilityService.Insert(CreateFacility(1));

            var facility = CreateFacility(2);

            facility.Name = "Microwave";

            var result = await facilityService.CheckFacilityExist(facility.Name, facility.Id);

            // Assert
            Assert.False(result);

            context.Database.EnsureDeleted();
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
