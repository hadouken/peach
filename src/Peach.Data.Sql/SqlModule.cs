using System.Configuration;
using Autofac;
using Autofac.Core;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Peach.Core.Runtime;
using Peach.Data.Sql.Repositories;

namespace Peach.Data.Sql
{
    public class SqlModule : Module
    {
        private readonly ILifetimeProvider _lifetimeProvider;
        private readonly string _connectionString;

        public SqlModule(ILifetimeProvider lifetimeProvider)
        {
            _lifetimeProvider = lifetimeProvider;
            _connectionString = ConfigurationManager.ConnectionStrings["Hadouken"].ConnectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            // Register ISessionFactory
            _lifetimeProvider.SingletonScope(
                builder.RegisterInstance(BuildSessionFactory())
                .As<ISessionFactory>()
                .OnActivating(RunMigrations));

            // Register ISession
            _lifetimeProvider.RequestScope(builder.Register(c => c.Resolve<ISessionFactory>().OpenSession()));

            // Register ISchemaMigrator
            _lifetimeProvider.SingletonScope(builder.RegisterType<SqlSchemaMigrator>().As<ISchemaMigrator>());

            // Register repositories
            _lifetimeProvider.InstanceScope(builder.RegisterType<BlogRepository>().AsImplementedInterfaces());
            _lifetimeProvider.InstanceScope(builder.RegisterType<PluginRepository>().AsImplementedInterfaces());
        }

        private void RunMigrations(IActivatingEventArgs<ISessionFactory> activatingEventArgs)
        {
            var migrator = activatingEventArgs.Context.Resolve<ISchemaMigrator>();
            migrator.Migrate();
        }

        private ISessionFactory BuildSessionFactory()
        {
            return Fluently
                .Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(_connectionString))
                .Mappings(mapConfig => mapConfig.FluentMappings.AddFromAssemblyOf<SqlModule>())
                .BuildSessionFactory();
        }
    }
}
