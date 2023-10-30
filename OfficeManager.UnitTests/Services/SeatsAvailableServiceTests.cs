using OfficeManager.Shared.Entities;
using OfficeManager.UnitTests.Mocks;
using BookingsServiceApi.Data;
using BookingsServiceApi.Areas.Services.Implementations;

namespace OfficeManager.UnitTests.Services
{
    public class SeatsAvailableServiceTests
    {
        public readonly MockDbContextFactoryBookings mockDbContextFactory;

        private ApplicationDbContext context;

        public SeatsAvailableServiceTests()
        {
            mockDbContextFactory = new MockDbContextFactoryBookings();

            context = mockDbContextFactory.CreateDbContext();

        }

        [Fact]
        public async Task CheckBookingSeatExist_ExistentBookingSeat_ShouldReturnEntity()
        {
            SeatsAvailableService seatsAvailableService = new SeatsAvailableService(context);

            await seatsAvailableService.Insert(CreateSeatsAvailable(1));

            var seatsAvailable = CreateSeatsAvailable(2);

            var result = await seatsAvailableService.CheckBookingSeatExist(seatsAvailable.RoomId, seatsAvailable.Date);

            // Assert
            Assert.NotNull(result);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task CheckBookingSeatExist_NonExistentBookingSeat_ShouldReturnEmpty()
        {
            SeatsAvailableService seatsAvailableService = new SeatsAvailableService(context);

            await seatsAvailableService.Insert(CreateSeatsAvailable(1));

            var seatsAvailable = CreateSeatsAvailable(2);

            seatsAvailable.RoomId = 2;

            var result = await seatsAvailableService.CheckBookingSeatExist(seatsAvailable.RoomId, seatsAvailable.Date);

            // Assert
            Assert.Null(result);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetSeatsUnavailableByDate_NoAvailableSeats_ShouldReturnEntityWithoutSeatsAvailable()
        {
            SeatsAvailableService seatsAvailableService = new SeatsAvailableService(context);

            var seatsAvailable = CreateSeatsAvailable(1);

            seatsAvailable.AvailableSeatsNumber = 0;

            await seatsAvailableService.Insert(seatsAvailable);

            var result = await seatsAvailableService.GetSeatsUnavailableByDate(seatsAvailable.Date);

            // Assert
            Assert.Single(result);
            Assert.Equal(seatsAvailable.Date, result.ElementAt(0).Date);
            Assert.Equal(seatsAvailable.AvailableSeatsNumber, result.ElementAt(0).AvailableSeatsNumber);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetSeatsUnavailableByDate_AvailableSeats_ShouldReturnEmpty()
        {
            SeatsAvailableService seatsAvailableService = new SeatsAvailableService(context);

            var newSeatsAvailable = CreateSeatsAvailable(1);

            await seatsAvailableService.Insert(newSeatsAvailable);

            var seatsAvailable = await seatsAvailableService.Get(1);

            var result = await seatsAvailableService.GetSeatsUnavailableByDate(seatsAvailable.Date);

            // Assert
            Assert.Empty(result);
            Assert.Equal(newSeatsAvailable.AvailableSeatsNumber, seatsAvailable.AvailableSeatsNumber);
            Assert.Equal(newSeatsAvailable.Date, seatsAvailable.Date);

            context.Database.EnsureDeleted();
        }

        private SeatsAvailable CreateSeatsAvailable(int id)
        {
            return new SeatsAvailable()
            {
                Id = id,
                Date = DateTime.Now,
                Hour = DateTime.Now,
                RoomId = 1,
                AvailableSeatsNumber = 10
            };
        }
    }
}

