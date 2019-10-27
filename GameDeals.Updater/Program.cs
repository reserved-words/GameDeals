using GameDeals.Data;
using GameDeals.Services;
using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

namespace GameDeals.Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new Logger();

            try
            {
                var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                var config = new ConfigurationBuilder()
                    .SetBasePath(directory)
                    .AddJsonFile("appsettings.json", false, true)
                    .Build();

                var service = new UpdateService(
                    new HtmlParser(),
                    new SyndicationFeedService(),
                    () => new UnitOfWork(config["UpdaterConnectionString"]),
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
