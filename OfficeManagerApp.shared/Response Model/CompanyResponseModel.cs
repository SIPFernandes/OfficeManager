using OfficeManager.Shared.Entities;

namespace OfficeManager.Shared.Response_Model
{
    public class CompanyResponseModel
    {
        ///<example>1</example>
        public int Id { get; set; }

        ///<example>Metyis</example>
        public string Name { get; set; }

        ///<example>In today’s era of disruption, setting direction is more critical than ever.</example>
        public string Description { get; set; }

        ///<example>Company image</example>
        public Image? Image { get; set; }
    }
}