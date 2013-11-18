using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Peach.Data.Sql;

namespace Peach.WebApi.Bootstrapping
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var builder = new ContainerBuilder();
            builder.RegisterModule<SqlModule>();
            builder.RegisterApiControllers(typeof (WebApiConfig).Assembly);

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
