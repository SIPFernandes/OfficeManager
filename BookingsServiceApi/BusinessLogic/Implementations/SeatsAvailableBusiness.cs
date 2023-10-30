using BookingsServiceApi.Areas.Services.Interfaces;
using BookingsServiceApi.BusinessLogic.Interfaces;
using OfficeManager.Shared.Entities;

namespace BookingsServiceApi.BusinessLogic.Implementations
{
    public class SeatsAvailableBusiness : GenericBusiness<ISeatsAvailableService, SeatsAvailable>, ISeatsAvailableBusiness
    {
        public SeatsAvailableBusiness(ISeatsAvailableService service, ILogger<SeatsAvailable> logger) : base(service, logger)
        {
        }

        public async Task UpdateRoomSeatsAvailability(SeatsAvailable seatsAvailable, bool increaseSeatsAvailable)
        {
            var checkSeatsAvailable = await Service.CheckBookingSeatExist(seatsAvailable.RoomId, seatsAvailable.Date);

            if (checkSeatsAvailable != null)
            {
                checkSeatsAvailable.AvailableSeatsNumber += increaseSeatsAvailable ? 1 : -1;

                await Service.Update(checkSeatsAvailable);
            }
            else
            {
                seatsAvailable.AvailableSeatsNumber--;

                await Service.Insert(seatsAvailable);
            }
        }

        public async Task<IEnumerable<SeatsAvailable>> GetSeatsUnavailableByDate(DateTime date)
        {
            return await Service.GetSeatsUnavailableByDate(date);
        }

        public async Task<SeatsAvailable> GetSeatsLeftByRoomIdDate(int roomId, DateTime date)
        {
            return await Service.GetSeatsLeftByRoomIdDate(roomId, date);
        }
    }
}
