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
    public class LocationController : GenericController<LocationModel, LocationRequestModel, LocationResponseModel>
    {
        public LocationController(ILocationBusiness locationBusiness, IMapper mapper) : base(locationBusiness, mapper)
        {
        }
    }
}
