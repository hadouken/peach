using System;
using System.Web.Mvc;
using Peach.Data;
using Peach.Data.Domain;
using Peach.Web.Models;

namespace Peach.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IUserRepository _userRepository;

        public BlogController(IBlogRepository blogRepository, IUserRepository userRepository)
        {
            _blogRepository = blogRepository;
            _userRepository = userRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult New()
        {
            return View();
        }

        [Authorize, HttpPost]
        public ActionResult New(NewBlogPostDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var currentUser = _userRepository.GetById(Convert.ToInt32(User.Identity.Name));

            var post = new BlogPost()
            {
                Content = dto.Content,
                PublishedDate = DateTime.Now,
                Slug = dto.Title,
                Title = dto.Title,
                User = currentUser
            };

            _blogRepository.Insert(post);

            return RedirectToAction("Index");
        }
    }
}