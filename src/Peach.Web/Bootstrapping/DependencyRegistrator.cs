using Autofac;
using Autofac.Integration.Mvc;
using Peach.Core;
using Peach.Core.Text;
using Peach.Data.Sql;

namespace Peach.Web.Bootstrapping
{
    public class DependencyRegistrator
    {
        public IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            var lifetimeProvider = new LifetimeProvider();

            // Register modules
            builder.RegisterModule(new SqlModule(lifetimeProvider));

            // Register MVC controllers
            builder.RegisterControllers(this.GetType().Assembly);

            // Register slug generator
            builder.RegisterType<SlugGenerator>().AsImplementedInterfaces().InstancePerDependency();

            // Register configuration
            builder.RegisterType<AppConfigConfiguration>().As<IConfiguration>().InstancePerHttpRequest();

            return builder.Build();
        }
    }
}