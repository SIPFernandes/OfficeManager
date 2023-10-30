using CompaniesServiceApi.Data;
using CompaniesServiceApi.Data.Services.Implementations;
using OfficeManager.Shared.Entities;
using OfficeManager.UnitTests.Mocks;

namespace OfficeManager.UnitTests.Services
{
    public class RoomServiceTests
    {
        public readonly MockDbContextFactoryCompanies mockDbContextFactory;

        private ApplicationDbContext context;

        public RoomServiceTests()
        {
            mockDbContextFactory = new MockDbContextFactoryCompanies();

            context = mockDbContextFactory.CreateDbContext();

        }
                
        [Fact]
        public async Task UpdateRoom_ShouldReturnEntity()
        {
            RoomService roomService = new RoomService(context);

            await roomService.Insert(CreateRoom(1));

            var room = await roomService.Get(1);

            room.Type = "Meeting room";

            await roomService.Update(room);

            // Assert
            Assert.Equal("Room 19", room.Name);
            Assert.Equal("Meeting room", room.Type);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task UpdateRoom_AddNewRoomFacilities_ShouldIncrementRoomFacilitiesSize()
        {
            //Arrange
            RoomService roomService = new RoomService(context);

            FacilityService facilityService = new FacilityService(context);

            var room = CreateRoom(1);
                        
            var facility_1 = CreateFacility(1, "Wifi");
            var facility_2 = CreateFacility(2, "Fridge");

            await facilityService.Insert(facility_1);
            await facilityService.Insert(facility_2);

            var roomFacility_1 = CreateRoomFacility(1, room, facility_1);
            var roomFacility_2 = CreateRoomFacility(1, room, facility_2);

            room.RoomFacilities = new List<RoomFacility>()
            {
                roomFacility_1, roomFacility_2
            };

            await roomService.Insert(room);

            var getRoom = await roomService.Get(1);

            //Act
            var facility_3 = CreateFacility(3, "Board");

            await facilityService.Insert(facility_3);

            var roomFacility_3 = CreateRoomFacility(1, room, facility_3);

            getRoom.RoomFacilities.Add(roomFacility_3);

            await roomService.Update(getRoom);

            // Assert
            Assert.Equal(3, getRoom.RoomFacilities.Count());
            Assert.Equal(room.Id, getRoom.Id);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task UpdateRoom_RemoveRoomFacilities_ShouldDecrementRoomFacilitiesSize()
        {
            //Arrange
            RoomService roomService = new RoomService(context);

            FacilityService facilityService = new FacilityService(context);

            var room = CreateRoom(1);

            var facility_1 = CreateFacility(1, "Wifi");
            var facility_2 = CreateFacility(2, "Fridge");

            await facilityService.Insert(facility_1);
            await facilityService.Insert(facility_2);

            var roomFacility_1 = CreateRoomFacility(1, room, facility_1);
            var roomFacility_2 = CreateRoomFacility(1, room, facility_2);

            room.RoomFacilities = new List<RoomFacility>()
            {
                roomFacility_1, roomFacility_2
            };

            await roomService.Insert(room);

            var getRoom = await roomService.Get(1);

            //Act
            getRoom.RoomFacilities.Remove(roomFacility_2);

            await roomService.Update(getRoom);

            // Assert
            Assert.Single(getRoom.RoomFacilities);
            Assert.Equal(room.Id, getRoom.Id);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task CheckRoomExist_ExistentRoom_ShouldReturnTrue()
        {
            RoomService roomService = new RoomService(context);

            await roomService.Insert(CreateRoom(1));

            var room = CreateRoom(2);

            var result = await roomService.CheckRoomExist(room.Id, room.Name, room.OfficeId);

            // Assert
            Assert.True(result);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task CheckRoomExist_NonExistentRoom_ShouldReturnFalse()
        {
            RoomService roomService = new RoomService(context);

            await roomService.Insert(CreateRoom(1));

            var room = CreateRoom(2);

            room.Name = "Room 17";

            var result = await roomService.CheckRoomExist(room.Id, room.Name, room.OfficeId);

            // Assert
            Assert.False(result);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetRoomsByOfficeId_ShouldReturnList()
        {
            RoomService roomService = new RoomService(context);

            await roomService.Insert(CreateRoom(1));

            var rooms = await roomService.GetRoomsByOfficeId(1);

            // Assert
            Assert.Single(rooms);

            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetRoomsByOfficeId_ShouldReturnEmptyList()
        {
            RoomService roomService = new RoomService(context);

            await roomService.Insert(CreateRoom(1));

            var rooms = await roomService.GetRoomsByOfficeId(2);

            // Assert
            Assert.Empty(rooms);

            context.Database.EnsureDeleted();
        }

        private Room CreateRoom(int id)
        {
            return new Room()
            {
                Id = id,
                Name = "Room 19",
                Type = "Working room",
                Description = "description",
                Images = new List<Image>()
                {
                    new Image()
                    {
                        File = "image"
                    }
                },
                OpeningHour = DateTime.Now,
                ClosingHour = DateTime.Now,
                Status = "Available",
                OfficeId = 1,
                Office = new Office()
                {
                    Name = "Faro Office",
                    Image = new Image()
                    {
                        File = "image"
                    },
                    Company = new Company()
                    {
                        Name = "Metyis",
                        Description = "description",
                        Image = new Image()
                        {
                            File = "image"
                        }
                    },
                    Location = new LocationModel()
                    {
                        Country = "Portugal",
                        City = "Faro"
                    },
                }
            };
        }

        private Facility CreateFacility(int id, string name)
        {
            return new Facility()
            {
                Id = id,
                Name = name,
                Image = new Image()
                {
                    File = "image"
                }
            };
        }

        private RoomFacility CreateRoomFacility(int id, Room room, Facility facility)
        {
            return new RoomFacility()
            {
                Id = id,
                RoomId = room.Id,
                Room = room,
                FacilityId = facility.Id,
                Facility = facility,
            };
        }
    }
}
