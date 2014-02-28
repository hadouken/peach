using System.Net.Http.Formatting;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Newtonsoft.Json.Serialization;
using Peach.Data.Sql;

namespace Peach.WebApi.Bootstrapping
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var lifetimeProvider = new LifetimeProvider();

            // Web API configuration and services
            var builder = new ContainerBuilder();
            builder.RegisterModule(new SqlModule(lifetimeProvider));
            builder.RegisterApiControllers(typeof (WebApiConfig).Assembly);

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // Only support JSON
            var formatter = new JsonMediaTypeFormatter
            {
                SerializerSettings = {ContractResolver = new CamelCasePropertyNamesContractResolver()}
            };

            config.Formatters.Clear();
            config.Formatters.Add(formatter);

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
