using CompaniesServiceApi.BusinessLogic.Interfaces;
using CompaniesServiceApi.Data.Services.Interfaces;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Exceptions;
using System.Net.Http;
using System.Security.Policy;

namespace CompaniesServiceApi.BusinessLogic.Implementations
{
    public class SeatBusiness : GenericBusiness<ISeatService, Seat>, ISeatBusiness
    {
        public SeatBusiness(ISeatService service, ILogger<SeatBusiness> logger) : base(service, logger)
        {
        }

        public override async Task<bool> Validate(Seat seat)
        {
            if (await Service.CheckSeatExist(seat.RoomId, seat.CoordinateX, seat.CoordinateY))
            {
                throw new EntityDuplicateException("seat in this room");
            }
            return true;
        }

        public async Task<Seat> GetSeatByCoordinates(int roomId, int coordinateX, int coordinateY)
{
            return await Service.GetSeatByCoordinates(roomId, coordinateX, coordinateY);
        }

        public async Task<IEnumerable<Seat>> GetSeatsByRoomId(int roomId)
{
            return await Service.GetSeatsByRoomId(roomId);
        }
    }
}