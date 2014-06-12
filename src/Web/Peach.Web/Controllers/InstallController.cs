using System.Web.Mvc;

namespace Peach.Web.Controllers
{
    public class InstallController : Controller
    {
        //
        [Route("install.ps1")]
        public string Index()
        {
            return "powershell";
        }
	}
}