using AutoMapper;
using CompaniesServiceApi.BusinessLogic.Interfaces;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;
using OfficeManager.Shared.Response_Model;
using Microsoft.AspNetCore.Mvc;
using OfficeManager.Shared.Exceptions;

namespace CompaniesServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeController : GenericController<Office, OfficeRequestModel, OfficeResponseModel>
    {
        private readonly IOfficeBusiness _officeBusiness;

        private readonly IMapper _mapper;

        public OfficeController(IOfficeBusiness officeBusiness, IMapper mapper) : base(officeBusiness, mapper)
        {
            _officeBusiness = officeBusiness;

            _mapper = mapper;
        }

        //GET ALL OFFICES IN THE COMPANY
        /// <summary>
        /// Gets all offices in the company.
        /// </summary>
        /// <response code="200">Success.</response>
        [HttpGet("Company/{companyId}")]
        public async Task<IActionResult> GetOfficesByCompanyId(int companyId)
        {
            if (companyId <= 0)
            {
                return BadRequest(new { error = "Unable to get the offices" });
            }

            var offices = await _officeBusiness.GetOfficesByCompanyId(companyId);

            return Ok(offices);
        }

        //CREATE OFFICE
        /// <summary>
        /// Creates a office.
        /// </summary>
        /// <response code="201">Office created successfully.</response>
        /// <response code="400">Unable to create the office due to validation error.</response>
        [HttpPost("Insert")]
        public async Task<ActionResult<int>> InsertOffice([FromBody] OfficeRequestModel office)
        {
            if (office == null || !ModelState.IsValid)
            {
                return BadRequest(new BadResponseModel
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage)
                });
            }

            var u = _mapper.Map<Office>(office);

            try
            {
                await _officeBusiness.Insert(u);
            }
            catch (EntityDuplicateException msg)
            {
                var errors = new List<string> { msg.Message };

                return BadRequest(new BadResponseModel { Errors = errors });
            }
            catch (Exception ex)
            {
                var errors = new List<string> { ex.Message };

                return BadRequest(new BadResponseModel { Errors = errors });
            }

            return Created("Office created successfully", u.Id);
        }
    }
}
