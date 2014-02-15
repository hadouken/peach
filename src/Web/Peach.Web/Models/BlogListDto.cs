using Peach.Data.Domain;

namespace Peach.Web.Models
{
    public class BlogListDto
    {
        public BlogPost[] BlogPosts { get; set; }

        public int CurrentPage { get; set; }

        public bool HasPreviousPage { get; set; }

        public bool HasNextPage { get; set; }
    }
}