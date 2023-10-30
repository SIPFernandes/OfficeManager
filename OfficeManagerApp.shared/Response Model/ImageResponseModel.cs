namespace OfficeManager.Shared.Response_Model
{
    public class ImageResponseModel
    {
        ///<example>1</example>
        public int Id { get; set; }

        ///<example>Some source link</example>
        public string File { get; set; }

        /// <example>2</example>
        public int? RoomId { get; set; }
    }
}
