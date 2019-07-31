using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace DatabaseMigrater
{
    public static class CreateUsers
    {
        public static void Run(string connectionString, string databaseName, string domainName, string webAppUser, string serviceUser)
        {
            var sqlScriptLocation = new DirectoryInfo(Assembly.GetExecutingAssembly().Location).Parent;
            var sqlScriptPath = Path.Combine(sqlScriptLocation.FullName, "CreateUsers.sql");

            if (!string.IsNullOrEmpty(domainName))
            {
                webAppUser = $"{domainName}\\{webAppUser}";
                serviceUser = $"{domainName}\\{serviceUser}";
            }

            var sql = File.ReadAllText(sqlScriptPath)
                .Replace("$(DatabaseName)", databaseName)
                .Replace("$(WebAppUser)", webAppUser)
                .Replace("$(ServiceUser)", serviceUser);

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(sql, connection) { CommandType = CommandType.Text })
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
