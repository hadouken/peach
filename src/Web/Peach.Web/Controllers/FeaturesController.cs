using System.Web.Mvc;
using Peach.Core;
using Peach.Data;

namespace Peach.Web.Controllers
{
    public sealed class FeaturesController : PeachController
    {
        public FeaturesController(IConfiguration configuration, IUserRepository userRepository)
            : base(configuration, userRepository)
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}