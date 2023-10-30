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
    public class RoomController : GenericController<Room, RoomRequestModel, RoomResponseModel>
    {
        private readonly IRoomBusiness _roomBusiness;

        private readonly IRoomFacilityBusiness _roomFacilityBusiness;

        public RoomController(IRoomBusiness roomBusiness, IRoomFacilityBusiness roomFacilityBusiness, IMapper mapper) : base(roomBusiness, mapper)
        {
            _roomBusiness = roomBusiness;

            _roomFacilityBusiness = roomFacilityBusiness;
        }

        //GET ALL ROOMS IN THE OFFICE
        /// <summary>
        /// Gets all rooms in the office.
        /// </summary>
        /// <response code="200">Success.</response>
        [HttpGet("Office/{officeId}")]
        public async Task<IActionResult> GetRoomsByOfficeId(int officeId)
        {
            if (officeId <= 0)
            {
                return BadRequest(new { error = "Unable to get the rooms" });
            }

            var rooms = await _roomBusiness.GetRoomsByOfficeId(officeId);

            return Ok(rooms);
        }

        //GET ALL AVAILABLE ROOMS
        /// <summary>
        /// Gets all available rooms.
        /// </summary>
        /// <response code="200">Success.</response>
        [HttpGet("Availables/{roomsIds}")]
        public async Task<IActionResult> GetRoomsByOfficeId(string roomsIds)
        {
            if (roomsIds == null)
            {
                return BadRequest(new { error = "Unable to get the rooms" });
            }

            IList<int> roomsIdsList = roomsIds.Split(',').Select(Int32.Parse).ToList();

            var rooms = await _roomBusiness.GetAvailableRooms(roomsIdsList);

            return Ok(rooms);
        }

        //GET ALL AVAILABLE ROOMS FROM OFFICE
        /// <summary>
        /// Gets all available rooms from office.
        /// </summary>
        /// <response code="200">Success.</response>
        [HttpGet("Availables/{officeId}/{unabailableRoomsIds}")]
        public async Task<IActionResult> GetAvailableRoomsByOfficeId(int officeId, string unabailableRoomsIds)
        {
            if (unabailableRoomsIds == null)
            {
                return BadRequest(new { error = "Unable to get the rooms" });
            }

            IList<int> unavailableRoomsIdsList = unabailableRoomsIds.Split(',').Select(Int32.Parse).ToList();

            var rooms = await _roomBusiness.GetAvailableRoomsByOfficeId(officeId, unavailableRoomsIdsList);

            return Ok(rooms);
        }

        //GET ALL FACILITIES IN THE ROOM
        /// <summary>
        /// Gets all facilities in the room.
        /// </summary>
        /// <response code="200">Success.</response>
        [HttpGet("RoomFacilities/{roomId}")]
        public async Task<IActionResult> GetFacilities(int roomId)
        {
            if (roomId <= 0)
            {
                return BadRequest(new { error = "Unable to get the services" });
            }

            var roomFacilities = await _roomFacilityBusiness.GetFacilities(roomId);

            return Ok(roomFacilities);
        }

        //CREATE RROM
        /// <summary>
        /// Creates a room.
        /// </summary>
        /// <response code="201">Room created successfully.</response>
        /// <response code="400">Unable to create the room due to validation error.</response>
        [HttpPost]
        public override async Task<ActionResult<RoomResponseModel>> Insert([FromBody] RoomRequestModel room)
        {
            if (room == null || !ModelState.IsValid)
            {
                return BadRequest(new BadResponseModel
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage)
                });
            }

            var u = new Room()
            {
                Name = room.Name,
                Type = room.Type,
                Description = room.Description,
                OpeningHour = room.OpeningHour,
                ClosingHour = room.ClosingHour,
                Status = room.Status,
                OfficeId = room.OfficeId,
                RoomFacilities = room.RoomFacilities.Select(x => new RoomFacility()
                {
                    Id = x.Id,
                    RoomId = x.RoomId,
                    FacilityId = x.FacilityId
                }).ToList(),
                Images = new List<Image>(),
                Seats = room.Seats.Select(x => new Seat()
                {
                    Id = x.Id,
                    RoomId = x.RoomId,
                    Name = x.Name,
                    CoordinateX = x.CoordinateX,
                    CoordinateY = x.CoordinateY
                }).ToList()
            };

            foreach(var image in room.Images)
            {
                u.Images.Add(new Image() { File = image });
            }

            try
            {
                await _roomBusiness.Insert(u);
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

            return Created("Entity created successfully", room);
        }

        //UPDATE ROOM BY ID
        /// <summary>
        /// Updates a specific room by id.
        /// </summary>
        /// <response code="201">Room updated successfully.</response>
        /// <response code="400">Unable to update the room due to validation error.</response>
        [HttpPut("{id}")]
        public override async Task<ActionResult<RoomResponseModel>> Update(int id, [FromBody] RoomRequestModel room)
        {
            if (id <= 0 || room == null || !ModelState.IsValid)
            {
                return BadRequest(new BadResponseModel
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage)
                });
            }

            var u = new Room()
            {
                Id = id,
                Name = room.Name,
                Type = room.Type,
                Description = room.Description,
                OpeningHour = room.OpeningHour,
                ClosingHour = room.ClosingHour,
                Status = room.Status,
                OfficeId = room.OfficeId,
                RoomFacilities = room.RoomFacilities.Select(x => new RoomFacility()
                {
                    Id = x.Id,
                    RoomId = x.RoomId,
                    FacilityId = x.FacilityId
                }).ToList(),
                Images = room.Images.Select(x => new Image()
                {
                    RoomId = id,
                    File = x
                }).ToList(),
                Seats = room.Seats.Select(x => new Seat()
                {
                    Id = x.Id,
                    RoomId = x.RoomId,
                    Name = x.Name,
                    CoordinateX = x.CoordinateX,
                    CoordinateY = x.CoordinateY
                }).ToList()
            };

            try
            {
                await _roomBusiness.Update(u);
            }
            catch (EntityDuplicateException msg)
            {
                var errors = new List<string> { msg.Message };

                return BadRequest(new BadResponseModel
                {
                    Errors = errors
                });
            }
            catch (Exception ex)
            {
                var errors = new List<string> { ex.Message };

                return BadRequest(new BadResponseModel { Errors = errors });
            }

            return Ok();

        }

        //GET IMAGES BY ROOMID
        /// <summary>
        /// Get images by roomId.
        /// </summary>
        /// <response code="200">Success.</response>
        [HttpGet("Images/{roomIds}")]
        public async Task<IActionResult> GetRoomsByIds(string roomIds)
        {
            if (roomIds == null)
            {
                return BadRequest(new { error = "Unable to get the rooms" });
            }

            IList<int> imageRoomsList = roomIds.Split(',').Select(Int32.Parse).ToList();

            var imageRooms = await _roomBusiness.GetRoomsByIds(imageRoomsList);

            return Ok(imageRooms);
        }
    }
}
