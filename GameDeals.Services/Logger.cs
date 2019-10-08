using System;
using System.IO;
using GameDeals.Core.Interfaces;

namespace GameDeals.Services
{
    public class Logger : ILogger
    {
        public void Log(Exception ex)
        {
            var directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var path = Path.Combine(directory, "GameDeals", "errors.log");
            Directory.CreateDirectory(path);
            File.AppendAllText(path, ex.Message);
            File.AppendAllText(path, ex.StackTrace);
        }
    }
}
