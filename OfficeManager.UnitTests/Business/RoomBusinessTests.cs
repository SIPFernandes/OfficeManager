using CompaniesServiceApi.BusinessLogic.Implementations;
using CompaniesServiceApi.Data.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Exceptions;

namespace OfficeManager.UnitTests.Business
{
    public class RoomBusinessTests
    {
        private ILogger<RoomBusiness> logger;

        [Fact]
        public async Task ValidateRoom_ExistentRoom_ShouldReturnTrue()
        {
            Mock<IRoomService> roomMock = new Mock<IRoomService>();

            Room room = CreateRoom(1);

            roomMock.Setup(x => x.CheckRoomExist(room.Id, room.Name, room.Id)).ReturnsAsync(false);

            RoomBusiness roomBusiness = new RoomBusiness(roomMock.Object, logger);

            var result = await roomBusiness.Validate(room);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ValidateRoom_NonExistentRoom_ShouldThrowEntityDuplicateException()
        {
            Mock<IRoomService> roomMock = new Mock<IRoomService>();

            Room room = CreateRoom(1);

            roomMock.Setup(x => x.CheckRoomExist(room.Id, room.Name, room.Id)).ReturnsAsync(true);

            RoomBusiness roomBusiness = new RoomBusiness(roomMock.Object, logger);

            // Assert
            await Assert.ThrowsAsync<EntityDuplicateException>(() => roomBusiness.Validate(room));
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
    }
}
