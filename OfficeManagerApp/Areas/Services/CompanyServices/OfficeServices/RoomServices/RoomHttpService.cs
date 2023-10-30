using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;
using OfficeManagerApp.Areas.Services.CompanyServices.OfficeServices.RoomServices.FacilityServices;
using OfficeManagerApp.Areas.Services.CompanyServices.OfficeServices.RoomServices.SeatServices;
using OfficeManagerApp.Areas.Services.Implementations;
using OfficeManagerApp.Data.Constants;
using System.Net.Http.Json;

namespace OfficeManagerApp.Areas.Services.CompanyServices.OfficeServices.RoomServices
{
    public class RoomHttpService : GenericHttpService<Room, RoomRequestModel, Room>, IRoomHttpService
    {
        public IFacilityHttpService FacilityHttpService { get => _facilityHttpService; }
        private readonly IFacilityHttpService _facilityHttpService;

        public ISeatHttpService SeatHttpService { get => _seatHttpService; }
        private readonly ISeatHttpService _seatHttpService;

        public override string Url { get => @AppConst.ApiUrlConst.CompaniesApi.RoomUrl; set => base.Url = value; }
        public string UrlRoom = @AppConst.ApiUrlConst.CompaniesApi.RoomOfficeUrl;

        public RoomHttpService(HttpClient httpClient) : base(httpClient)
        {            
            _facilityHttpService = new FacilityHttpService(_httpClient);
            _seatHttpService = new SeatHttpService(_httpClient);
        }

        public async Task<IList<Room>> GetRoomsByOfficeId(int officeId)
        {
            var rooms = await _httpClient.GetFromJsonAsync<List<Room>>(UrlRoom + "/" + officeId);

            return rooms;
        }
        
        public async Task<IList<Room>> GetAvailableRooms(IList<int> roomsIds)
        {
            string roomIdsString = string.Join(",", roomsIds);

            if(string.IsNullOrEmpty(roomIdsString))
            {
                roomIdsString = "0";
            }

            var rooms = await _httpClient.GetFromJsonAsync<List<Room>>(Url + "/Availables/" + roomIdsString);

            return rooms;
        }
        
        public async Task<IList<Room>> GetAvailableRoomsByOfficeId(int officeId, IList<int> unavailableRoomsIds)
        {
            string roomIdsString = string.Join(",", unavailableRoomsIds);

            if(string.IsNullOrEmpty(roomIdsString))
            {
                roomIdsString = "0";
            }

            var rooms = await _httpClient.GetFromJsonAsync<List<Room>>(Url + "/Availables/" + officeId + "/" + roomIdsString);

            return rooms;
        }

        public async Task<IList<RoomFacility>> GetFacilities(int roomId)
        {
            var roomFacilities = await _httpClient.GetFromJsonAsync<List<RoomFacility>>(UrlRoom + "/" + roomId);

            return roomFacilities;
        }

        public async Task<IDictionary<int, Room>> GetRoomsByIds(IList<int> roomIds)
        {
            string roomIdsString = string.Join(",", roomIds);

            if (string.IsNullOrEmpty(roomIdsString))
            {
                return null;
            }

            var imageRooms = await _httpClient.GetFromJsonAsync<IDictionary<int, Room>>(Url + "/Images/" + roomIdsString);

            return imageRooms;
        }
    }
}
