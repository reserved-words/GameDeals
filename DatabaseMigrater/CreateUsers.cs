﻿using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseMigrater
{
    public static class CreateUsers
    {
        public static void Run(string connectionString, string databaseName, string webAppUser, string serviceUser)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@DatabaseName", databaseName),
                    new SqlParameter("@ServiceUser", serviceUser),
                    new SqlParameter("@WebAppUser", webAppUser)
                };

                using (var command = new SqlCommand("[GameDeals].[CreateUsers]", connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddRange(parameters.ToArray());
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
