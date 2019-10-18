using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDeals.Mapper
{
    public class Service : GameDeals.Core.Interfaces.IMapper
    {
        private readonly Lazy<AutoMapper.IMapper> _autoMapper;

        public Service()
        {
            _autoMapper = new Lazy<AutoMapper.IMapper>(() => new AutoMapper.Mapper(ConfigureAutoMapper()));
        }

        private MapperConfiguration ConfigureAutoMapper()
        {
            return new MapperConfiguration(cfg => {
                cfg.AddProfile<RssProfile>();
            });
        }

        public T1 Map<T2, T1>(T2 source, T1 destination = null) where T2 : class where T1 : class
        {
            return _autoMapper.Value.Map(source, destination);
        }
    }
}
