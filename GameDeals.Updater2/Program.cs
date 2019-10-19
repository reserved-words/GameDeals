using GameDeals.Data2;
using GameDeals.Services;
using System;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GameDeals.Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new Logger();

            try
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())            
                    .AddJsonFile("appsettings.json", false, true)
                    .Build();

                var service = new UpdateService(
                    new HtmlParser(),
                    new SyndicationFeedService(),
                    () => new UnitOfWork(config["UpdaterConnectionString"], config["UpdaterSchemaName"]),
                    logger);

                service.UpdatePosts();
            }
            catch (Exception ex)
            {
                logger.Log(ex);
            }
        }
    }
}
