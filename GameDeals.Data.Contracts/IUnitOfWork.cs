using System;

namespace GameDeals.Data.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : class;
        void Save();
    }
}