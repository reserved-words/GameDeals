using System;

namespace GameDeals.Core.Interfaces
{
    public interface ILogger
    {
        void Log(string appName, Exception ex);
    }
}
