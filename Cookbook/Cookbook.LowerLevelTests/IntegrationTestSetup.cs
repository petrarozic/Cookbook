using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Cookbook.LowerLevelTests
{
    public class IntegrationTestSetup : IntegrationTestBase, IDisposable
    {
        public IntegrationTestSetup()
        {
            DestroyDatabase();
            CreateDatabase();
        }
        public void Dispose()
        {
            DestroyDatabase();
        }
        private static void CreateDatabase()
        {
            ExecuteSqlCommand(Master, $@"
                CREATE DATABASE [CookbookDBIntegrationTest]
                ON (NAME = 'CookbookDBIntegrationTest',
                FILENAME = '{Filename}')");

            var context = GivenAppContext();
            context.Database.Migrate();
        }

        private static void DestroyDatabase()
        {
            var fileNames = ExecuteSqlQuery(Master, @"
                SELECT [physical_name] FROM [sys].[master_files]
                WHERE [database_id] = DB_ID('CookbookDBIntegrationTest')",
                row => (string)row["physical_name"]);

            if (fileNames.Any())
            {
                ExecuteSqlCommand(Master, @"
                    ALTER DATABASE [CookbookDBIntegrationTest] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                    EXEC sp_detach_db 'CookbookDBIntegrationTest'");

                fileNames.ForEach(File.Delete);
            }
        }

        private static void ExecuteSqlCommand(
            SqlConnectionStringBuilder connectionStringBuilder,
            string commandText)
        {
            using (var connection = new SqlConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = commandText;
                    command.ExecuteNonQuery();
                }
            }
        }

        private static List<T> ExecuteSqlQuery<T>(
            SqlConnectionStringBuilder connectionStringBuilder,
            string queryText,
            Func<SqlDataReader, T> read)
        {
            var result = new List<T>();
            using (var connection = new SqlConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = queryText;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(read(reader));
                        }
                    }
                }
            }
            return result;
        }

        private static SqlConnectionStringBuilder Master =>
            new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                InitialCatalog = "master",
                IntegratedSecurity = true
            };

        private static string Filename => Path.Combine(
            Path.GetDirectoryName(
                typeof(IntegrationTestSetup).GetTypeInfo().Assembly.Location),
            "CookbookDBIntegrationTest.mdf");
    }
}
