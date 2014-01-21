using System.Web.Mvc;
using Peach.Data;

namespace Peach.Web.Controllers
{
    public class PluginsController : PeachController
    {
        public PluginsController(IUserRepository userRepository)
            : base(userRepository) { }

        public ActionResult Index()
        {
            return View();
        }
	}
}