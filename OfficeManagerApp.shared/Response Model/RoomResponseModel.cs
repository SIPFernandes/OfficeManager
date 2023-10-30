using OfficeManager.Shared.Entities;

namespace OfficeManager.Shared.Response_Model
{
    public class RoomResponseModel
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>Room 19</example>
        public string Name { get; set; }

        /// <example>Meeting room</example>
        public string Type { get; set; }

        /// <example>Great open sapce!</example>
        public string Description { get; set; }

        /// <example>08:00:00</example>
        public DateTime OpeningHour { get; set; }

        /// <example>19:00:00</example>
        public DateTime ClosingHour { get; set; }

        /// <example>Available</example>
        public string Status { get; set; }

        /// <example>true</example>
        public bool CanBook { get; set; }

        /// <example>2</example>
        public int OfficeId { get; set; }

        /// <example>Some source link</example>
        public IList<Image> Images { get; set; }

        public IEnumerable<RoomFacilityResponseModel> RoomFacilities { get; set; }
    }
}