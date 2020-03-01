using System;
using GameDeals.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using ErrorLogger = ErrorLog.Logger.Logger;

namespace GameDeals.Services
{
    public class Logger : ILogger
    {
        private readonly IConfiguration _config;

        public Logger(IConfiguration config)
        {
            _config = config;
        }

        public void Log(Exception ex)
        {
            var loggingConfig = _config.GetSection("Logging");

            var url = loggingConfig["Url"];
            var clientId = loggingConfig["ClientId"];
            var clientSecret = loggingConfig["ClientSecret"];
            var tokenEndpoint = loggingConfig["TokenEndpoint"];

            var logger = new ErrorLogger(clientId, clientSecret, tokenEndpoint, url);

            logger.Log(ex);
        }
    }
}
