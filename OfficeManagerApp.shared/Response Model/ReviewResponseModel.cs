namespace OfficeManager.Shared.Response_Model
{
    public class ReviewResponseModel
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>4,5</example>
        public float Classification { get; set; }

        /// <example>Incredible!</example>
        public string Text { get; set; }

        /// <example>2</example>
        public int RoomId { get; set; }

        /// <example>3</example>
        public int UserId { get; set; }
    }
}