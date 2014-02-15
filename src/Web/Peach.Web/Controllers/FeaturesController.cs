using System.Web.Mvc;
using Peach.Core;
using Peach.Data;
using Peach.Data.Domain;

namespace Peach.Web.Controllers
{
    public class FeaturesController : PeachController
    {
        public FeaturesController(IConfiguration configuration, IUserRepository userRepository)
            : base(configuration, userRepository) { }

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