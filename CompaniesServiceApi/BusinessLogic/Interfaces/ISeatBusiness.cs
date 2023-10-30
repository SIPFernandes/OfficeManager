using OfficeManager.Shared.Entities;


namespace CompaniesServiceApi.BusinessLogic.Interfaces
{
    public interface ISeatBusiness : IGenericBusiness<Seat>
    {
        Task<Seat> GetSeatByCoordinates(int roomId, int coordinateX, int coordinateY);
        Task<IEnumerable<Seat>> GetSeatsByRoomId(int roomId);
    }
}
