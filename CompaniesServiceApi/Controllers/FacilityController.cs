using AutoMapper;
using CompaniesServiceApi.BusinessLogic.Interfaces;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;
using OfficeManager.Shared.Response_Model;
using Microsoft.AspNetCore.Mvc;

namespace CompaniesServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityController : GenericController<Facility, FacilityRequestModel, FacilityResponseModel>
    {
        public FacilityController(IFacilityBusiness facilityBusiness, IMapper mapper) : base(facilityBusiness, mapper)
        {
        }
    }
}
