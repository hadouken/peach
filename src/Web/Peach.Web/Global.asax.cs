using System.Web.Mvc;
using System.Web.Routing;
using Autofac.Integration.Mvc;
using Peach.Web.Bootstrapping;

namespace Peach.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // Register routes
            new RouteRegistrator().RegisterRoutes(RouteTable.Routes);

            // Build DI container
            var dependencyRegistrator = new DependencyRegistrator();
            var container = dependencyRegistrator.BuildContainer();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
