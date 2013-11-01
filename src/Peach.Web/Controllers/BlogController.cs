using System.Web.Mvc;
using Peach.Data;

namespace Peach.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}