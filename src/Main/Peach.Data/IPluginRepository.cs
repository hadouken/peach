using Peach.Core.Data;
using Peach.Data.Domain;

namespace Peach.Data
{
    public interface IPluginRepository : IRepository<Plugin>
    {
        Plugin GetByName(string name);
    }
}