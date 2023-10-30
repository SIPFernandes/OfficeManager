using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Exceptions;
using Microsoft.Extensions.Logging;
using Moq;
using BookingsServiceApi.BusinessLogic.Implementations;
using BookingsServiceApi.Data.Services.Interfaces;
using BookingsServiceApi.BusinessLogic.Interfaces;

namespace OfficeManager.UnitTests.Business
{
    public class BookingBusinessTests
    {
        private ILogger<BookingBusiness> logger;
        private ISeatsAvailableBusiness seatsAvailableBusiness;

        [Fact]
        public async Task ValidateBooking_ExistentBooking_ShouldReturnTrue()
        {
            Mock<IBookingService> bookingMock = new Mock<IBookingService>();

            Booking booking = CreateBooking(1);

            bookingMock.Setup(x => x.CheckBookingExist(booking.Id, booking.UserId, booking.RoomId, booking.Date)).ReturnsAsync(false);

            BookingBusiness bookingBusiness = new BookingBusiness(seatsAvailableBusiness, bookingMock.Object, logger);

            var result = await bookingBusiness.Validate(booking);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ValidateBooking_NonExistentBooking_ShouldThrowEntityDuplicateException()
        {
            Mock<IBookingService> bookingMock = new Mock<IBookingService>();

            Booking booking = CreateBooking(1);

            bookingMock.Setup(x => x.CheckBookingExist(booking.Id, booking.UserId, booking.RoomId, booking.Date)).ReturnsAsync(true);

            BookingBusiness bookingBusiness = new BookingBusiness(seatsAvailableBusiness, bookingMock.Object, logger);

            // Assert
            await Assert.ThrowsAsync<EntityDuplicateException>(() => bookingBusiness.Validate(booking));
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
