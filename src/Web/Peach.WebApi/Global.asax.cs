using System.Web;
using System.Web.Http;
using Peach.WebApi.Bootstrapping;

namespace Peach.WebApi
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
