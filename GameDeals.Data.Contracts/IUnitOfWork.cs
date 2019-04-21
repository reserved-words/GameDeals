﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameDeals.Data.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : class;
        void Save();
    }
}