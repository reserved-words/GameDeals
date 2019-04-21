using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDeals.Core.Interfaces
{
    public interface IMapper
    {
        T1 Map<T2, T1>(T2 source, T1 destination = null) where T1 : class where T2 : class;
    }
}
