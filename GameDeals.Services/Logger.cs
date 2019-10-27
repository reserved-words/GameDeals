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
            var path = Path.Combine(directory, "GameDeals");
            Directory.CreateDirectory(path);
            var filepath = Path.Combine(path, "errors.log");
            File.AppendAllText(filepath, ex.Message);
            File.AppendAllText(filepath, ex.StackTrace);
        }
    }
}
