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
    public class RoomFacilityController : GenericController<RoomFacility, RoomFacilityRequestModel, RoomFacilityResponseModel>
    {
        public RoomFacilityController(IRoomFacilityBusiness roomFacility, IMapper mapper) : base(roomFacility, mapper)
        {
        }
    }
}
