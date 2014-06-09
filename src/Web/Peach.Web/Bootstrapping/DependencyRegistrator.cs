using Autofac;
using Autofac.Integration.Mvc;
using Octokit;
using Peach.Core;
using Peach.Core.Text;
using Peach.Data.Sql;
using Peach.IO.Azure;

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
            builder.RegisterModule(new AzureModule(lifetimeProvider));

            // Register MVC controllers
            builder.RegisterControllers(this.GetType().Assembly);

            // Register slug generator
            builder.RegisterType<SlugGenerator>().AsImplementedInterfaces().InstancePerDependency();

            // Register configuration
            builder.RegisterType<AppConfigConfiguration>().As<IConfiguration>().InstancePerHttpRequest();

            // Register GitHub client
            builder.Register<IGitHubClient>(c => new GitHubClient(new ProductHeaderValue("Hadouken")));

            return builder.Build();
        }
    }
}