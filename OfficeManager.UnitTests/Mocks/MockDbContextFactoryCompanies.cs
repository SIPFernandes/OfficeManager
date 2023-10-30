﻿using CompaniesServiceApi.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace OfficeManager.UnitTests.Mocks
{
    public class MockDbContextFactoryCompanies : IDbContextFactory<ApplicationDbContext>
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public readonly SqliteConnection connection;

        public MockDbContextFactoryCompanies()
        {            
            connection = new SqliteConnection("DataSource=:memory:");

            connection.Open();

            _options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(connection).Options;
        }

        public ApplicationDbContext CreateDbContext()
        {
            var appContext = new ApplicationDbContext(_options);

            appContext.Database.EnsureCreated();

            return appContext;
        }
    }
}


