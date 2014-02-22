using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Peach.Data.Domain;

namespace Peach.Data.Sql.Repositories
{
    public class PluginRepository : Repository<Plugin>, IPluginRepository
    {
        public PluginRepository(ISession session) : base(session)
        {
        }

        public Plugin GetByName(string name)
        {
            return Session.Query<Plugin>().Where(p => p.Name == name).SingleOrDefault();
        }
    }
}
