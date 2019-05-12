using GameDeals.Core.Interfaces;
using GameDeals.Data;
using GameDeals.Services;
using System;
using System.Configuration;
using System.Net.Http;

namespace GameDeals.Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new Logger();

            try
            {
                var service = new UpdateService(
                    new HtmlParser(), 
                    new SyndicationFeedService(), 
                    () => new UnitOfWork("GameDealsUpdater"),
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
