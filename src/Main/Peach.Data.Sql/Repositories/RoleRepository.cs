using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Peach.Data.Domain;

namespace Peach.Data.Sql.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(ISession session) : base(session)
        {
        }

        public Role GetByName(string name)
        {
            return Session.Query<Role>().SingleOrDefault(r => r.Name == name);
        }
    }
}
