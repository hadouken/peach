using System.Web.Mvc;
using Peach.Data;

namespace Peach.Web.Controllers
{
    public class HomeController : PeachController
    {
        public HomeController(IUserRepository userRepository)
            : base(userRepository) { }

        public ActionResult Index()
        {
            return View();
        }
	}
}