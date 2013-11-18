using NHibernate;
using Peach.Data.Domain;

namespace Peach.Data.Sql.Repositories
{
    public class BlogRepository : Repository<BlogPost>, IBlogRepository
    {
        public BlogRepository(ISession session) : base(session)
        {
        }
    }
}
