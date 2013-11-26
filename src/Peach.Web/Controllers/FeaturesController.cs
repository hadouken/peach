using System.Web.Mvc;
using Peach.Data.Domain;

namespace Peach.Web.Controllers
{
    public class FeaturesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = Role.ContentWriter)]
        public ActionResult New()
        {
            return View();
        }

        [Authorize(Roles = Role.ContentWriter)]
        [HttpPost]
        public ActionResult New(object model)
        {
            return View();
        }
    }
}