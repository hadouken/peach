using System.Web.Mvc;
using Peach.Data;
using Peach.Data.Domain;

namespace Peach.Web.Controllers
{
    public class FeaturesController : PeachController
    {
        public FeaturesController(IUserRepository userRepository)
            : base(userRepository) { }

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