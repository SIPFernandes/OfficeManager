using BookingsServiceApi.Data;
using BookingsServiceApi.Data.Services.Implementations;
using OfficeManager.Shared.Entities;
using OfficeManager.UnitTests.Mocks;

namespace OfficeManager.UnitTests.Services
{
    public class BookingServiceTests
    {
        public readonly MockDbContextFactoryBookings mockDbContextFactory;

        private ApplicationDbContext context;

        public BookingServiceTests()
        {
            mockDbContextFactory = new MockDbContextFactoryBookings();

            context = mockDbContextFactory.CreateDbContext();

        }

        [Fact]
        public async Task UpdateBooking_ShouldReturnEntity()
        {
            BookingService bookingService = new BookingService(context);

            await bookingService.Insert(CreateBooking(1));

            var booking = await bookingService.Get(1);

            booking.RoomId = 2;

            await bookingService.Update(booking);

            // Assert
            Assert.Equal(1, booking.UserId);
            Assert.Equal(2, booking.RoomId);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task CheckBookingExist_ExistentBooking_ShouldReturnTrue()
        {
            BookingService bookingService = new BookingService(context);

            await bookingService.Insert(CreateBooking(1));

            var booking = CreateBooking(2);

            booking.RoomId = 2;

            var result = await bookingService.CheckBookingExist(booking.Id, booking.UserId, booking.RoomId, booking.Date);

            // Assert
            Assert.True(result);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task CheckBookingExist_NonExistentBooking_ShouldReturnFalse()
        {
            BookingService bookingService = new BookingService(context);

            await bookingService.Insert(CreateBooking(1));

            var booking = CreateBooking(2);

            var result = await bookingService.CheckBookingExist(booking.Id, booking.UserId, booking.RoomId, booking.Date);

            // Assert
            Assert.False(result);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetBookingsByDateRoomId_ShouldReturnBooking()
        {
            BookingService bookingService = new BookingService(context);

            await bookingService.Insert(CreateBooking(1));

            var booking = await bookingService.Get(1);

            var result = await bookingService.GetBookingsByDateRoomId(booking.RoomId, booking.Date);

            // Assert
            Assert.Single(result);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetBookingsByDateRoomId_ShouldReturnEmpty()
        {
            BookingService bookingService = new BookingService(context);

            int roomId = 2;
            DateTime date = DateTime.Now;

            var result = await bookingService.GetBookingsByDateRoomId(roomId, date);

            // Assert
            Assert.Empty(result);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetBookingsByDate_ShouldReturnBooking()
        {
            BookingService bookingService = new BookingService(context);

            await bookingService.Insert(CreateBooking(1));

            var booking = await bookingService.Get(1);

            var result = await bookingService.GetBookingsByDate(booking.Date);

            // Assert
            Assert.Single(result);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetBookingsByDate_ShouldReturnEmpty()
        {
            BookingService bookingService = new BookingService(context);

            DateTime date = DateTime.Now;

            var result = await bookingService.GetBookingsByDate(date);

            // Assert
            Assert.Empty(result);

            context.Database.EnsureDeleted();
        }

        private Booking CreateBooking(int id)
        {
            return new Booking()
            {
                Id = id,
                Date = DateTime.Now,
                Hour = DateTime.Now,
                RoomId = 1,
                UserId = 1,
                SeatId = 1
            };
        }
    }
}
