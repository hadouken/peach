using System.Web.Mvc;

namespace Peach.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}