using NHibernate;
using Peach.Data.Domain;

namespace Peach.Data.Sql.Repositories
{
    public class PluginRepository : Repository<Plugin>, IPluginRepository
    {
        public PluginRepository(ISession session) : base(session)
        {
        }
    }
}
