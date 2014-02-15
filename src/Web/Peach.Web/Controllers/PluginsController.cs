using System.Web.Mvc;
using Peach.Core;
using Peach.Data;

namespace Peach.Web.Controllers
{
    public class PluginsController : PeachController
    {
        public PluginsController(IConfiguration configuration, IUserRepository userRepository)
            : base(configuration, userRepository) { }

        public ActionResult Index()
        {
            return View();
        }
	}
}