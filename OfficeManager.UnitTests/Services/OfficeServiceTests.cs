using CompaniesServiceApi.Data;
using CompaniesServiceApi.Data.Services.Implementations;
using OfficeManager.Shared.Entities;
using OfficeManager.UnitTests.Mocks;

namespace OfficeManager.UnitTests.Services
{
    public class OfficeServiceTests
    {
        public readonly MockDbContextFactoryCompanies mockDbContextFactory;

        private ApplicationDbContext context;

        public OfficeServiceTests()
        {
            mockDbContextFactory = new MockDbContextFactoryCompanies();

            context = mockDbContextFactory.CreateDbContext();

        }

        private OfficeService CreateOfficeService()
        {
            RoomService roomService = new RoomService(context);

            return new OfficeService(roomService, context);
        }

        [Fact]
        public async Task UpdateOffice_ShouldReturnEntity()
        {
            OfficeService officeService = CreateOfficeService();

            await officeService.Insert(CreateOffice(1));

            var office = await officeService.Get(1);

            await officeService.Update(office);

            // Assert
            Assert.Equal("Faro Office", office.Name);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task CheckOfficeExist_ExistentOffice_ShouldReturnTrue()
        {
            OfficeService officeService = CreateOfficeService();

            await officeService.Insert(CreateOffice(1));

            var office = CreateOffice(2);

            var result = await officeService.CheckOfficeExist(office.Id, office.Name, office.CompanyId);

            // Assert
            Assert.True(result);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task CheckOfficeExist_NonExistentOffice_ShouldReturnFalse()
        {
            OfficeService officeService = CreateOfficeService();

            await officeService.Insert(CreateOffice(1));

            var office = CreateOffice(2);

            office.Name = "Lisbon Office";

            var result = await officeService.CheckOfficeExist(office.Id, office.Name, office.CompanyId);

            // Assert
            Assert.False(result);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetOfficesByCompanyId_ShouldReturnList()
        {
            OfficeService officeService = CreateOfficeService();

            await officeService.Insert(CreateOffice(1));

            var offices = await officeService.GetOfficesByCompanyId(1);

            // Assert
            Assert.Single(offices);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetOfficesByCompanyId_ShouldReturnEmptyList()
        {
            OfficeService officeService = CreateOfficeService();

            await officeService.Insert(CreateOffice(1));

            var offices = await officeService.GetOfficesByCompanyId(2);

            // Assert
            Assert.Empty(offices);

            context.Database.EnsureDeleted();
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
