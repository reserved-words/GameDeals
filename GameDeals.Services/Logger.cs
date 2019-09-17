using System;
using System.IO;
using GameDeals.Core.Interfaces;

namespace GameDeals.Services
{
    public class Logger : ILogger
    {
        public void Log(Exception ex)
        {
            var directory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            var path = Path.Combine(directory, "GameDeals", "errors.log");
            File.AppendAllText(path, ex.Message);
            File.AppendAllText(path, ex.StackTrace);
        }
    }
}
