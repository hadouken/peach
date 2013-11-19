using Peach.Core.Data;
using Peach.Data.Domain;

namespace Peach.Data
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByClaimedIdentifier(string claimedIdentifier);

        User GetByUserName(string userName);
    }
}