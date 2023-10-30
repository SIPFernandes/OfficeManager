using System.ComponentModel.DataAnnotations;

namespace OfficeManager.Shared.Request_Model
{
    public class ReviewRequestModel
    {
        /// <example>4.5</example>
        [Required(ErrorMessage = "Classification is required.")]
        public float Classification { get; set; }

        /// <example>Awesome! :)</example>
        [Required(ErrorMessage = "Text is required.")]
        public string Text { get; set; }

        /// <example>4</example>
        [Required(ErrorMessage = "RoomId is required.")]
        public int RoomId { get; set; }

        /// <example>3</example>
        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }
    }
}