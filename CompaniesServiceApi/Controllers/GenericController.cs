using AutoMapper;
using CompaniesServiceApi.BusinessLogic.Interfaces;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Response_Model;
using Microsoft.AspNetCore.Mvc;
using OfficeManager.Shared.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace CompaniesServiceApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]    
    [ApiController]
    public class GenericController<TEntity, TRequestModel, TResponseModel> : ControllerBase where TEntity : BaseEntity
    {
        private readonly IGenericBusiness<TEntity> _genericBusiness;

        private readonly IMapper _mapper;

        public GenericController(IGenericBusiness<TEntity> genericBusiness, IMapper mapper)
        {
            _genericBusiness = genericBusiness;

            _mapper = mapper;
        }

        //GET ALL ENTITIES
        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <response code="200">Success.</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _genericBusiness.GetAll();

            return Ok(entities);
        }

        //GET ENTITY BY ID
        /// <summary>
        /// Gets a specific entity by id.
        /// </summary>
        /// <response code="200">Success.</response>
        /// <response code="400">Unable to get the entity due to validation error.</response>
        /// <response code="404">Entity not found!</response>
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TResponseModel>> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { error = "Unable to get the entity" });
            }

            var getEntity = await _genericBusiness.Get(id);

            return Ok(getEntity);
        }

        //CREATE ENTITY
        /// <summary>
        /// Creates a entity.
        /// </summary>
        /// <response code="201">Entity created successfully.</response>
        /// <response code="400">Unable to create the entity due to validation error.</response>
        [HttpPost]
        public virtual async Task<ActionResult<TResponseModel>> Insert([FromBody] TRequestModel entity)
        {
            if (entity == null || !ModelState.IsValid)
            {
                return BadRequest(new BadResponseModel
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage)
                });
            }

            var u = _mapper.Map<TEntity>(entity);

            try
            {
                await _genericBusiness.Insert(u);
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

            return Created("Entity created successfully", u);
        }

        //UPDATE ENTITY BY ID
        /// <summary>
        /// Updates a specific entity by id.
        /// </summary>
        /// <response code="201">Entity updated successfully.</response>
        /// <response code="400">Unable to update the entity due to validation error.</response>
        [HttpPut("{id}")]
        public virtual async Task<ActionResult<TResponseModel>> Update(int id, [FromBody] TRequestModel entity)
        {
            if (id <= 0 || entity == null || !ModelState.IsValid)
            {
                return BadRequest(new BadResponseModel
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage)
                });
            }

            var u = _mapper.Map<TEntity>(entity);
            u.Id = id;
            
            try
            {
                await _genericBusiness.Update(u);
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

            return Ok();

        }

        //DELETE ENITTY BY ID
        /// <summary>
        /// Deletes a specific entity by id.
        /// </summary>
        /// <response code="200">Success.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Entity not found!</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { error = "Unable to delete the entity" });
            }

            try
            {
                await _genericBusiness.DeleteById(id);
            }
            catch (Exception ex)
            {
                var errors = new List<string> { ex.Message };
                return BadRequest(new BadResponseModel { Errors = errors });
            }

            return Ok();
        }
    }
}