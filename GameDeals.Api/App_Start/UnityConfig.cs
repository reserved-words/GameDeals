using GameDeals.Core.Interfaces;
using GameDeals.Data;
using GameDeals.Data.Contracts;
using GameDeals.Mapper;
using GameDeals.Services;
using System;

using Unity;

namespace GameDeals.Api
{
    public static class UnityConfig
    {
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        public static IUnityContainer Container => container.Value;

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IRssService, RssService>();
            container.RegisterType<IUpdateService, UpdateService>();
            container.RegisterType<ILogger, Logger>();
            container.RegisterType<IMapper, Service>();

            // container.RegisterFactory<GetCurrentDateTime>(c => new GetCurrentDateTime(() => DateTime.Now));
            container.RegisterFactory<Func<IUnitOfWork>>(c => new Func<IUnitOfWork>(() => new UnitOfWork("GameDeals")));
        }
    }
}