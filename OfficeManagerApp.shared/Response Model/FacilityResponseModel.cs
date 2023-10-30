using OfficeManager.Shared.Entities;

namespace OfficeManager.Shared.Response_Model
{
    public class FacilityResponseModel
    {
        ///<example>1</example>
        public int Id { get; set; }

        /// <example>Wi-Fi</example>
        public string Name { get; set; }

        /// <example>Facility image</example>
        public Image Image { get; set; }
    }
}