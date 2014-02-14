using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Peach.Data.Domain;

namespace Peach.Data.Sql.Repositories
{
    public class ReleaseRepository : Repository<Release>, IReleaseRepository
    {
        public ReleaseRepository(ISession session)
            : base(session)
        {
        }

        public Release GetLatest()
        {
            return Session.Query<Release>()
                .OrderByDescending(r => r.ReleaseDate)
                .FirstOrDefault();
        }
    }
}
