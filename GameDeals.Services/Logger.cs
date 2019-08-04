using System;
using System.IO;
using System.Text;
using GameDeals.Core.Interfaces;

namespace GameDeals.Services
{
    public class Logger : ILogger
    {
        public void Log(Exception ex)
        {
            var path = @"errors.log";
            File.AppendAllText(path, ex.Message);
            File.AppendAllText(path, ex.StackTrace);
        }
    }
}
