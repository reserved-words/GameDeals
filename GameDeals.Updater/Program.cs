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
            Logger logger = null;

            try
            {
                var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                var config = new ConfigurationBuilder()
                    .SetBasePath(directory)
                    .AddJsonFile("appsettings.json", false, true)
                    .Build();

                logger = new Logger(config);

                var service = new UpdateService(
                    new HtmlParser(),
                    new SyndicationFeedService(),
                    () => new UnitOfWork(config["UpdaterConnectionString"]),
                    logger);

                service.UpdatePosts();
            }
            catch (Exception ex)
            {
                if (logger != null)
                {
                    logger.Log(ex);
                }
            }
        }
    }
}
