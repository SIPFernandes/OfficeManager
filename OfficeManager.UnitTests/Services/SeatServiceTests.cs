using CompaniesServiceApi.Data;
using CompaniesServiceApi.Data.Services.Implementations;
using OfficeManager.Shared.Entities;
using OfficeManager.UnitTests.Mocks;

namespace OfficeManager.UnitTests.Services
{
    public class SeatServiceTests
    {
        public readonly MockDbContextFactoryCompanies mockDbContextFactory;

        private ApplicationDbContext context;

        public SeatServiceTests()
        {
            mockDbContextFactory = new MockDbContextFactoryCompanies();

            context = mockDbContextFactory.CreateDbContext();

        }

        [Fact]
        public async Task UpdateSeat_ShouldReturnEntity()
        {
            SeatService seatService = new SeatService(context);

            await seatService.Insert(CreateSeat(1));

            var seat = await seatService.Get(1);

            seat.CoordinateY = 2;

            await seatService.Update(seat);

            // Assert
            Assert.Equal(2, seat.CoordinateY);
            Assert.Equal(1, seat.RoomId);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task CheckSeatExist_ExistentSeat_ShouldReturnTrue()
        {
            SeatService seatService = new SeatService(context);

            await seatService.Insert(CreateSeat(1));

            var seat = CreateSeat(2);

            var result = await seatService.CheckSeatExist(seat.RoomId, seat.CoordinateX, seat.CoordinateY);

            // Assert
            Assert.True(result);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task CheckSeatExist_NonExistentSeat_ShouldReturnFalse()
        {
            SeatService seatService = new SeatService(context);

            await seatService.Insert(CreateSeat(1));

            var seat = CreateSeat(2);

            seat.CoordinateX = 2;

            var result = await seatService.CheckSeatExist(seat.RoomId, seat.CoordinateX, seat.CoordinateY);

            // Assert
            Assert.False(result);

            context.Database.EnsureDeleted();
        }

        private Seat CreateSeat(int id)
        {
            return new Seat()
            {
                Id = id,
                Name = "Table",
                CoordinateX = 1,
                CoordinateY = 1,
                RoomId = 1,
                Room = new Room()
                {
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
                    },
                }
            };
        }
    }
}
