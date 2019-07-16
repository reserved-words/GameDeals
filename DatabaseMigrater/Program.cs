using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Sql;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Reflection;
using Configuration = GameDeals.Data.Migrations.Configuration;

namespace DatabaseMigrater
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
                throw new Exception("No connection string supplied");

            var connectionString = args[0];

            try
            {
                using (var context = new DbContext(connectionString))
                {
                    var config = new Configuration();
                    config.TargetDatabase = new DbConnectionInfo(connectionString, "System.Data.SqlClient");
                    var migrator = new MigratorLoggingDecorator(new DbMigrator(config), new GameDealsMigrationsLogger());
                    if (migrator.GetPendingMigrations().Any())
                    {
                        migrator.Update();
                    }
                }
            }
            catch (Exception exc)
            {
                var failedToMigrateException = new Exception("Failed to apply migrations", exc);
                Console.WriteLine($"Didn't succeed in applying migrations: {exc.Message}");
                System.IO.File.AppendAllText("errors.log", string.Format("{0:dd/MM/yyyy HH:mm}: {1}", DateTime.Now, exc.Message));
                System.IO.File.AppendAllText("errors.log", string.Format("{0:dd/MM/yyyy HH:mm}: {1}", DateTime.Now, exc.StackTrace));
                throw failedToMigrateException;
            }
        }

        private static void RunMigration(DbContext context, DbMigration migration)
        {
            var prop = migration.GetType().GetProperty("Operations", BindingFlags.NonPublic | BindingFlags.Instance);
            if (prop != null)
            {
                IEnumerable<MigrationOperation> operations = prop.GetValue(migration) as IEnumerable<MigrationOperation>;
                var generator = new SqlServerMigrationSqlGenerator();
                var statements = generator.Generate(operations, "2008");
                foreach (MigrationStatement item in statements)
                    context.Database.ExecuteSqlCommand(item.Sql);
            }
        }
    }

    public class GameDealsMigrationsLogger : MigrationsLogger
    {
        public override void Info(string message)
        {
            Console.WriteLine("INFO: " + message);
        }

        public override void Verbose(string message)
        {
            Console.WriteLine("VERBOSE: " + message);
        }

        public override void Warning(string message)
        {
            Console.WriteLine("WARNING: " + message);
        }
    }
}
