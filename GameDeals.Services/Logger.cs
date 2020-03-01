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
            var logger = new ErrorLogger(_config);
            logger.Log(ex);
        }
    }
}
