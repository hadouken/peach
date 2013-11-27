using Peach.Data.Domain;

namespace Peach.Web.Extensions
{
    public static class BlogPostExtensions
    {
        public static object GetRouteData(this BlogPost blogPost)
        {
            return
                new
                {
                    year = blogPost.PublishedDate.Year.ToString(),
                    month = blogPost.PublishedDate.Month.ToString().PadLeft(2, '0'),
                    slug = blogPost.Slug
                };
        }
    }
}