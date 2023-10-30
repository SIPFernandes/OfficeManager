using OfficeManager.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace OfficeManager.Shared.Entities
{
    public class BookingModel
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string? UserId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = BookingConst.Error.BiggerThenZero)]
        public int CompanyId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = BookingConst.Error.BiggerThenZero)]
        public int LocationId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = BookingConst.Error.BiggerThenZero)]
        public int RoomId { get; set; }
        public int Chair { get; set; }
        public DateTime Date { get; set; }
    }
}
