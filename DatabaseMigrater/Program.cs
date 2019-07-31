using System;
using System.Linq;

namespace DatabaseMigrater
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 4 || args.Length > 5)
                throw new Exception("Incorrect numer of arguments");

            var connectionString = args[0];
            var databaseName = args[1];
            var webAppUser = args[2];
            var serviceUser = args[3];

            var domainName = args.Length == 5
                ? args[4]
                : "";

            try
            {
                CreateSchema.Run(connectionString);
            }
            catch (Exception exc)
            {
                throw new Exception("Failed to apply migrations", exc);
            }

            try
            {
                CreateUsers.Run(connectionString, databaseName, domainName, webAppUser, serviceUser);
            }
            catch (Exception exc)
            {
                throw new Exception("Failed to create users", exc);
            }
        }
    }


}
