using Cookbook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Cookbook.LowerLevelTests
{
    public class IntegrationTestBase
    {
        protected static AppDbContext GivenAppContext()
        {
            return new AppDbContext(new DbContextOptionsBuilder()
                .UseSqlServer(CookbookDB.ConnectionString)
                .Options);
        }

        private static SqlConnectionStringBuilder CookbookDB =>
            new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                InitialCatalog = "Cookbook",
                IntegratedSecurity = true
            };
    }
}
