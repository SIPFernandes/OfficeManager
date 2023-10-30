using OfficeManager.Shared.Entities;

namespace OfficeManager.Shared.Response_Model
{
    public class OfficeResponseModel
    {
        ///<example>1</example>
        public int Id { get; set; }

        /// <example>Faro Office</example>
        public string Name { get; set; }

        /// <example>Office image</example>
        public Image Image { get; set; }

        /// <example>2</example>
        public int CompanyId { get; set; }

        /// <example>3</example>
        public int LocationId { get; set; }
    }
}