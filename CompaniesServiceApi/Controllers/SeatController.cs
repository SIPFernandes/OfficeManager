using AutoMapper;
using CompaniesServiceApi.BusinessLogic.Implementations;
using CompaniesServiceApi.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Request_Model;
using OfficeManager.Shared.Response_Model;

namespace CompaniesServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : GenericController<Seat, SeatRequestModel, SeatResponseModel>
    {
        private readonly ISeatBusiness _seatBusiness;

        public SeatController(ISeatBusiness seatBusiness, IMapper mapper) : base(seatBusiness, mapper)
{
            _seatBusiness = seatBusiness;
        }

        //GET SEAT BY COORDINATES
        /// <summary>
        /// Get seat by coordinates.
        /// </summary>
        /// <response code="200">Success.</response>
        [HttpGet("Room/{roomId}/{coordinateX}/{coordinateY}")]
        public async Task<IActionResult> GetSeatbyCoordinates(int roomId, int coordinateX, int coordinateY)
        {
            if (roomId <= 0)
            {
                return BadRequest(new { error = "Unable to get the seat" });
            }

            var seat = await _seatBusiness.GetSeatByCoordinates(roomId, coordinateX, coordinateY);

            return Ok(seat);
        }

        //GET SEAT BY COORDINATES
        /// <summary>
        /// Get seat by coordinates.
        /// </summary>
        /// <response code="200">Success.</response>
        [HttpGet("Room/{roomId}")]
        public async Task<IActionResult> GetSeatbyCoordinates(int roomId)
        {
            if (roomId <= 0)
            {
                return BadRequest(new { error = "Unable to get the seat" });
            }

            var seats = await _seatBusiness.GetSeatsByRoomId(roomId);

            return Ok(seats);
        }
    }
}
