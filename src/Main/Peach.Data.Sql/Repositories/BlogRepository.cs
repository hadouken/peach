using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Peach.Data.Domain;

namespace Peach.Data.Sql.Repositories
{
    public class BlogRepository : Repository<BlogPost>, IBlogRepository
    {
        public BlogRepository(ISession session) : base(session)
        {
        }

        public BlogPost GetByYearMonthAndSlug(int year, int month, string slug)
        {
            return
                Session.Query<BlogPost>()
                    .SingleOrDefault(
                        post =>
                            post.PublishedDate.Year == year && post.PublishedDate.Month == month && post.Slug == slug);
        }
    }
}
