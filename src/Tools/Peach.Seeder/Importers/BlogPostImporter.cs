using System;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Peach.Data;
using Peach.Data.Domain;

namespace Peach.Seeder.Importers
{
    public class BlogPostImporter : IImporter
    {
        public class JsonPost
        {
            public string Title { get; set; }

            public string Slug { get; set; }

            public DateTime Date { get; set; }

            public string Content { get; set; }
        }

        private readonly IBlogRepository _blogRepository;
        private readonly IUserRepository _userRepository;

        public BlogPostImporter(IBlogRepository blogRepository, IUserRepository userRepository)
        {
            _blogRepository = blogRepository;
            _userRepository = userRepository;
        }

        public void Import()
        {
            var defaultAuthor = _userRepository.GetById(1);
            var files = Directory.GetFiles("data/blogposts/", "*.json");

            foreach (var file in files)
            {
                var post = JsonConvert.DeserializeObject<JsonPost>(File.ReadAllText(file));

                var existing = _blogRepository.GetByYearMonthAndSlug(post.Date.Year, post.Date.Month, post.Slug);
                if (existing != null) continue;

                var blogPost = new BlogPost
                {
                    Content = post.Content,
                    PublishedDate = post.Date,
                    Slug = post.Slug,
                    Title = post.Title,
                    User = defaultAuthor
                };

                Console.WriteLine("\tInserting post {0}", post.Title);

                _blogRepository.Insert(blogPost);
            }
        }
    }
}
