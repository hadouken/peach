using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Peach.Core;
using Peach.Core.Text;
using Peach.Data;
using Peach.Data.Domain;
using Peach.Web.Models;

namespace Peach.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISlugGenerator _slugGenerator;

        private readonly int _pageSize = 10;

        public BlogController(IBlogRepository blogRepository, IUserRepository userRepository, ISlugGenerator slugGenerator)
        {
            _blogRepository = blogRepository;
            _userRepository = userRepository;
            _slugGenerator = slugGenerator;

            var configPageSize = ConfigurationManager.AppSettings["Blog:PageSize"];

            if (!String.IsNullOrEmpty(configPageSize))
            {
                _pageSize = Convert.ToInt32(configPageSize);
            }
        }

        [Route("blog/")]
        [Route("blog/page/{page}")]
        public ActionResult Index(int page = 1)
        {
            var posts = _blogRepository.GetPage(post => post.PublishedDate, SortOrder.Descending, page - 1, _pageSize);
            var count = _blogRepository.Count();

            var model = new BlogListDto
            {
                BlogPosts = posts.ToArray(),
                CurrentPage = page,
                HasNextPage = (page > 1),
                HasPreviousPage = (count > page*_pageSize),
            };

            return View(model);
        }

        [Route("blog/{year}/{month}/{slug}")]
        public ActionResult Details(int year, int month, string slug)
        {
            var post = _blogRepository.GetByYearMonthAndSlug(year, month, slug);

            if (post == null)
                return HttpNotFound();

            return View(post);
        }

        [Authorize(Roles = Role.ContentWriter)]
        public ActionResult New()
        {
            return View();
        }

        [Authorize(Roles = Role.ContentWriter)]
        [HttpPost]
        public ActionResult New(NewBlogPostDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var currentUser = _userRepository.GetById(Convert.ToInt32(User.Identity.Name));

            var post = new BlogPost
            {
                Content = dto.Content,
                PublishedDate = DateTime.Now,
                Slug = _slugGenerator.Generate(dto.Title),
                Title = dto.Title,
                User = currentUser
            };

            _blogRepository.Insert(post);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = Role.ContentWriter)]
        public ActionResult Edit(int id)
        {
            var blogPost = _blogRepository.GetById(id);

            if (blogPost == null)
                return HttpNotFound();

            var model = new EditBlogPostDto
            {
                Id = blogPost.Id,
                Content = blogPost.Content,
                Title = blogPost.Title
            };

            return View(model);
        }

        [Authorize(Roles = Role.ContentWriter)]
        [HttpPost]
        public ActionResult Edit(int id, EditBlogPostDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var blogPost = _blogRepository.GetById(id);

            if (blogPost == null)
                return HttpNotFound();

            blogPost.Content = dto.Content;
            blogPost.Title = dto.Title;

            _blogRepository.Update(blogPost);

            return RedirectToAction("Details",
                new
                {
                    year = blogPost.PublishedDate.Year.ToString(),
                    month = blogPost.PublishedDate.Month.ToString().PadLeft(2, '0'),
                    slug = blogPost.Slug
                });
        }
    }
}