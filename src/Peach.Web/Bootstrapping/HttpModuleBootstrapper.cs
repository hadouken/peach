using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Peach.Web.Bootstrapping;
using Peach.Web.HttpModules;

[assembly: PreApplicationStartMethod(typeof(HttpModuleBootstrapper), "Bootstrap")]

namespace Peach.Web.Bootstrapping
{
    public class HttpModuleBootstrapper
    {
        public static void Bootstrap()
        {
            DynamicModuleUtility.RegisterModule(typeof (UserAuthModule));
        }
    }
}