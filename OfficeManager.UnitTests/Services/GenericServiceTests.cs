using CompaniesServiceApi.Data;
using CompaniesServiceApi.Data.Services.Implementations;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using OfficeManager.UnitTests.Mocks;
using System.Collections;

namespace OfficeManager.UnitTests.Services
{
    public class GenericServiceTests
    {
        private readonly MockDbContextFactoryCompanies _mockDbContextFactory;

        private static ApplicationDbContext _dbContext;

        public GenericServiceTests()
        {
            _mockDbContextFactory = new MockDbContextFactoryCompanies();

            _dbContext = _mockDbContextFactory.CreateDbContext();
        }

        [Theory]
        [ClassData(typeof(TestDataService))]
        public async void GetAllEntities_ShouldReturnEmpty<TEntity>
            (Func<ApplicationDbContext, GenericService<TEntity>> getService) where TEntity : BaseEntity
        {
            var service = getService(_dbContext);

            var locations = await service.GetAll();

            // Assert
            Assert.Empty(locations);

            _dbContext.Database.EnsureDeleted();
        }

        [Theory]
        [ClassData(typeof(TestDataServiceEntity))]
        public async Task GetAllEntities_ShouldReturnOneLocation<TEntity>
            (Func<ApplicationDbContext, GenericService<TEntity>> getService, TEntity entity) where TEntity : BaseEntity
        {
            var service = getService(_dbContext);

            await service.Insert(entity);

            var locations = await service.GetAll();

            // Assert
            Assert.Single(locations);

            _dbContext.Database.EnsureDeleted();
        }

        [Theory]
        [ClassData(typeof(TestDataService))]
        public async Task GetEntityById_ShouldThrowEntityDoesNotExistException<TEntity>
            (Func<ApplicationDbContext, GenericService<TEntity>> getService) where TEntity : BaseEntity
        {
            var service = getService(_dbContext);

            // Assert
            await Assert.ThrowsAsync<EntityDoesNotExistException>(() => service.Get(1));

            _dbContext.Database.EnsureDeleted();
        }

        [Theory]
        [ClassData(typeof(TestDataServiceEntity))]
        public async Task GetEntityById_ShouldReturnEntity<TEntity>
            (Func<ApplicationDbContext, GenericService<TEntity>> getService, TEntity entity) where TEntity : BaseEntity
        {
            var service = getService(_dbContext);

            await service.Insert(entity);

            var getEntity = await service.Get(1);

            // Assert
            Assert.Equal(entity, getEntity);

            _dbContext.Database.EnsureDeleted();
        }

        [Theory]
        [ClassData(typeof(TestDataServiceEntity))]
        public async Task InsertEntity_ShouldReturnAllEntities<TEntity>
            (Func<ApplicationDbContext, GenericService<TEntity>> getService, TEntity entity) where TEntity : BaseEntity
        {
            var service = getService(_dbContext);

            await service.Insert(entity);

            var entities = await service.GetAll();

            // Assert
            Assert.Single(entities);

            _dbContext.Database.EnsureDeleted();
        }

        [Theory]
        [ClassData(typeof(TestDataServiceEntityWithoutFK))]
        public async Task InsertEntityWithoutFK_ShouldThrowDBUpdateException<TEntity>
            (Func<ApplicationDbContext, GenericService<TEntity>> getService, TEntity entity) where TEntity : BaseEntity
        {
            var service = getService(_dbContext);

            // Assert
            await Assert.ThrowsAsync<DbUpdateException>(() => service.Insert(entity));

            _dbContext.Database.EnsureDeleted();
        }

        [Theory]
        [ClassData(typeof(TestDataServiceEntity))]
        public async Task DeleteEntityById_ShouldReturnEmpty<TEntity>
            (Func<ApplicationDbContext, GenericService<TEntity>> getService, TEntity entity) where TEntity : BaseEntity
        {
            var service = getService(_dbContext);

            await service.Insert(entity);

            await service.DeleteById(1);

            var entities = await service.GetAll();

            // Assert
            Assert.Empty(entities);

            _dbContext.Database.EnsureDeleted();
        }

        [Theory]
        [ClassData(typeof(TestDataService))]
        public async Task DeleteEntityById_ShouldThrowEntityDoesNotExistException<TEntity>
            (Func<ApplicationDbContext, GenericService<TEntity>> getService) where TEntity : BaseEntity
        {
            var service = getService(_dbContext);

            // Assert
            await Assert.ThrowsAsync<EntityDoesNotExistException>(() => service.DeleteById(1));

            _dbContext.Database.EnsureDeleted();
        }

        [Theory]
        [ClassData(typeof(TestDataServiceEntity))]
        public async Task GetEntityDeletedById_ShouldThrowEntityDoesNotExistException<TEntity>
            (Func<ApplicationDbContext, GenericService<TEntity>> getService, TEntity entity) where TEntity : BaseEntity
        {
            var service = getService(_dbContext);

            await service.Insert(entity);

            await service.DeleteById(1);

            // Assert
            await Assert.ThrowsAsync<EntityDoesNotExistException>(() => service.Get(1));

            _dbContext.Database.EnsureDeleted();
        }

        private class TestDataServiceEntity : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new()
            {
                new object[] {(ApplicationDbContext dbContext) => new
                    GenericService<LocationModel>(dbContext),
                    new LocationModel()
                    {
                        Country = "Portugal",
                        City = "Faro"
                    }
                },
                new object[] {(ApplicationDbContext dbContext) => new
                    GenericService<Company>(dbContext),
                    new Company()
                    {
                        Name = "Metyis",
                        Description = "description",
                        Image = new Image()
                        {
                            File = "image"
                        }
                    }
                },
                new object[] {(ApplicationDbContext dbContext) => new
                    GenericService<Office>(dbContext),
                    new Office()
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
                        }
                    }
                },
                new object[] {(ApplicationDbContext dbContext) => new
                    GenericService<Room>(dbContext),
                    new Room()
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
                },
                new object[] {(ApplicationDbContext dbContext) => new
                    GenericService<Facility>(dbContext),
                    new Facility()
                    {
                        Name = "WiFi",
                        Image = new Image()
                        {
                            File = "image"
                        }
                    }
                },
                new object[] {(ApplicationDbContext dbContext) => new
                    GenericService<Review>(dbContext),
                    new Review()
                    {
                        Classification = 4,
                        Text = "text",
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
                        },
                        UserId = 1
                    }
                },
                new object[] {(ApplicationDbContext dbContext) => new
                    GenericService<Seat>(dbContext),
                    new Seat()
                    {
                        Name = "Table",
                        CoordinateX = 1,
                        CoordinateY = 1,
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
                        },
                    }
                }
            };
            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private class TestDataService : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new()
            {
                new object[] {(ApplicationDbContext dbContext) => new
                    GenericService<LocationModel>(dbContext)
                },
                new object[] {(ApplicationDbContext dbContext) => new
                    GenericService<Company>(dbContext)
                },
                new object[] {(ApplicationDbContext dbContext) => new
                    GenericService<Office>(dbContext)
                },
                new object[] {(ApplicationDbContext dbContext) => new
                    GenericService<Room>(dbContext)
                },
                new object[] {(ApplicationDbContext dbContext) => new
                    GenericService<Facility>(dbContext)
                },
                new object[] {(ApplicationDbContext dbContext) => new
                    GenericService<Review>(dbContext)
                },
                new object[] {(ApplicationDbContext dbContext) => new
                    GenericService<Seat>(dbContext)
                }
            };
            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private class TestDataServiceEntityWithoutFK : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new()
            {
                new object[] {(ApplicationDbContext dbContext) => new
                    GenericService<Office>(dbContext),
                    new Office()
                    {
                        Name = "Faro Office",
                        Image = new Image()
                        {
                            File = "image"
                        },
                        CompanyId = 1,
                        LocationId = 1
                    }
                },
                new object[] {(ApplicationDbContext dbContext) => new
                    GenericService<Room>(dbContext),
                    new Room()
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
                        OfficeId = 1
                    }
                },
                new object[] {(ApplicationDbContext dbContext) => new
                    GenericService<Review>(dbContext),
                    new Review()
                    {
                        Classification = 4,
                        Text = "text",
                        RoomId = 1,
                        UserId = 1
                    }
                },
                new object[] {(ApplicationDbContext dbContext) => new
                    GenericService<Seat>(dbContext),
                    new Seat()
                    {
                        CoordinateX = 1,
                        CoordinateY = 1,
                        RoomId = 1
                    }
                }
            };
            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }


}