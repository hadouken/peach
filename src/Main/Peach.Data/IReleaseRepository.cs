using Peach.Core.Data;
using Peach.Data.Domain;

namespace Peach.Data
{
    public interface IReleaseRepository : IRepository<Release>
    {
        Release GetLatest();
    }
}
