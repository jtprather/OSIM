using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Ninject.Modules;
using Ninject.Activation;
using OSIM.Core.Entities;
using OSIM.Core.Persistence;

namespace OSIM.IntegrationTests
{
    public class IntegrationTestModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IItemTypeRepository>().To<ItemTypeRepository>();
            Bind<ISessionFactory>().ToProvider(new IntegrationTestSessionFactoryProvider());
        }
    }
    public class IntegrationTestSessionFactoryProvider : Provider<ISessionFactory>
    {
        protected override ISessionFactory CreateInstance(IContext context)
        {
            var sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                    .ConnectionString(c => c.Is(ConfigurationManager.AppSettings["localDb"])).ShowSql())
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ItemTypeMap>()
                            .ExportTo(@"C:\Temp"))
                        .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
                        .BuildSessionFactory();

            return sessionFactory;
        }
    }
}
