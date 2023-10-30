using CompaniesServiceApi.Data;
using CompaniesServiceApi.Data.Services.Implementations;
using OfficeManager.Shared.Entities;
using OfficeManager.UnitTests.Mocks;

namespace OfficeManager.UnitTests.Services
{
    public class RoomFacilityServiceTests
    {
        public readonly MockDbContextFactoryCompanies mockDbContextFactory;

        private ApplicationDbContext context;

        public RoomFacilityServiceTests()
        {
            mockDbContextFactory = new MockDbContextFactoryCompanies();

            context = mockDbContextFactory.CreateDbContext();

        }

        [Fact]
        public async Task GetFacilities_ShouldReturnList()
        {
            RoomFacilityService roomFacilityService = new RoomFacilityService(context);

            await roomFacilityService.Insert(CreateRoomFacility(1));

            var roomFacilities = await roomFacilityService.GetFacilities(1);

            // Assert
            Assert.Single(roomFacilities);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetFacilities_ShouldReturnEmptyList()
        {
            RoomFacilityService roomFacilityService = new RoomFacilityService(context);

            await roomFacilityService.Insert(CreateRoomFacility(1));

            var roomFacilities = await roomFacilityService.GetFacilities(2);

            // Assert
            Assert.Empty(roomFacilities);

            context.Database.EnsureDeleted();
        }

        private RoomFacility CreateRoomFacility(int id)
        {
            return new RoomFacility()
            {
                Id = id,
                RoomId = 1,
                Room = new Room()
                {
                    Id = id,
                    Name = "Room 19",
                    Type = "Working room",
                    Description = "description",
                    Images = new List<Image>()
                {
                    new Image()
                    {
                        File = "image"
                    }
                },
                    OpeningHour = DateTime.Now,
                    ClosingHour = DateTime.Now,
                    Status = "Available",
                    OfficeId = 1,
                    Office = new Office()
                    {
                        Name = "Faro Office",
                        Image = new Image()
                        {
                            File = "image"
                        },
                        Company = new Company()
                        {
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
                        },
                    }
                },
                FacilityId = 1,
                Facility = new Facility()
                {
                    Id = id,
                    Name = "WiFi",
                    Image = new Image()
                    {
                        File = "image"
                    }
                }
            };
        }
    }
}
