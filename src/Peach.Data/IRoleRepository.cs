using Peach.Core.Data;
using Peach.Data.Domain;

namespace Peach.Data
{
    public interface IRoleRepository : IRepository<Role>
    {
        Role GetByName(string name);
    }
}
