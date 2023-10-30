using CompaniesServiceApi.BusinessLogic.Implementations;
using CompaniesServiceApi.Data.Services.Interfaces;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Exceptions;
using Microsoft.Extensions.Logging;
using Moq;

namespace OfficeManager.UnitTests.Business
{
    public class SeatBusinessTests
    {

        private ILogger<SeatBusiness> logger;

        [Fact]
        public async Task ValidateSeat_ExistentSeat_ShouldReturnTrue()
        {
            Mock<ISeatService> seatMock = new Mock<ISeatService>();

            Seat seat = CreateSeat(1);

            seatMock.Setup(x => x.CheckSeatExist(seat.RoomId, seat.CoordinateX, seat.CoordinateY)).ReturnsAsync(false);

            SeatBusiness seatBusiness = new SeatBusiness(seatMock.Object, logger);

            var result = await seatBusiness.Validate(seat);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ValidateSeat_NonExistentSeat_ShouldThrowEntityDuplicateException()
        {
            Mock<ISeatService> seatMock = new Mock<ISeatService>();

            Seat seat = CreateSeat(1);

            seatMock.Setup(x => x.CheckSeatExist(seat.RoomId, seat.CoordinateX, seat.CoordinateY)).ReturnsAsync(true);

            SeatBusiness seatBusiness = new SeatBusiness(seatMock.Object, logger);

            // Assert
            await Assert.ThrowsAsync<EntityDuplicateException>(() => seatBusiness.Validate(seat));
        }

        private Seat CreateSeat(int id)
        {
            return new Seat()
            {
                Id = id,
                CoordinateX = 1,
                CoordinateY = 1,
                RoomId = 1,
                Room = new Room()
                {
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
                    },
                }
            };
        }
    }
}
