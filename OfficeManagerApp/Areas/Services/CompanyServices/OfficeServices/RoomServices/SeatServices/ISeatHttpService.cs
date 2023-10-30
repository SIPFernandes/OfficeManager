using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;
using OfficeManagerApp.Areas.Services.Interfaces;

namespace OfficeManagerApp.Areas.Services.CompanyServices.OfficeServices.RoomServices.SeatServices
{
    public interface ISeatHttpService : IGenericHttpService<Seat, SeatRequestModel, Seat>
    {
        Task<Seat> GetSeatByCoordinates(int roomId, int x, int y);
        Task<IEnumerable<Seat>> GetSeatsByRoomId(int roomId);
    }
}
