using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Exceptions;
using BookingsServiceApi.BusinessLogic.Interfaces;
using BookingsServiceApi.Data.Services.Interfaces;

namespace BookingsServiceApi.BusinessLogic.Implementations
{
    public class BookingBusiness : GenericBusiness<IBookingService, Booking>, IBookingBusiness
    {
        private readonly ISeatsAvailableBusiness _seatsAvailableBusiness;
        public BookingBusiness(ISeatsAvailableBusiness seatsAvailableBusiness, IBookingService service, ILogger<BookingBusiness> logger) : base(service, logger)
        {
            _seatsAvailableBusiness = seatsAvailableBusiness;
        }

        public override async Task Insert(Booking entity)
        {
            await base.Insert(entity);

            var seatsAvailableRequestModel = new SeatsAvailable()
            {
                Date = entity.Date,
                Hour = entity.Hour,
                RoomId = entity.RoomId,
                AvailableSeatsNumber = entity.RoomSize,
            };

            await _seatsAvailableBusiness.UpdateRoomSeatsAvailability(seatsAvailableRequestModel, false);
        }

        public override async Task DeleteById(int id)
        {
            var booking = await Service.DeleteById(id);

            var seatsAvailableRequestModel = new SeatsAvailable()
            {
                Date = booking.Date,
                Hour = booking.Hour,
                RoomId = booking.RoomId
            };

            await _seatsAvailableBusiness.UpdateRoomSeatsAvailability(seatsAvailableRequestModel, true);
        }

        public override async Task<bool> Validate(Booking booking)
        {
            if (await Service.CheckBookingExist(booking.Id, booking.UserId, booking.RoomId, booking.Date))
            {
                throw new EntityDuplicateException("booking");
            }
            return true;
        }

        public async Task<IEnumerable<Booking>> GetBookingsByDateRoomId(int roomId, DateTime date)
        {
            return await Service.GetBookingsByDateRoomId(roomId, date);
        }

        public async Task<IEnumerable<Booking>> GetBookingsByDate(DateTime date)
        {
            return await Service.GetBookingsByDate(date);
        }
    }
}
