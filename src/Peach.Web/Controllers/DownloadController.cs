using System.Web.Mvc;
using Peach.Data;

namespace Peach.Web.Controllers
{
    public class DownloadController : PeachController
    {
        //
        // GET: /Download/
        public DownloadController(IUserRepository userRepository)
            : base(userRepository)
        {
        }

        public ActionResult Index()
        {
            return View();
        }
	}
}