using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Peach.Data.Domain;

namespace Peach.Data.Sql.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ISession session) : base(session)
        {
        }

        public User GetByClaimedIdentifier(string claimedIdentifier)
        {
            return Session.Query<User>().SingleOrDefault(user => user.ClaimedIdentifier == claimedIdentifier);
        }

        public User GetByUserName(string userName)
        {
            return Session.Query<User>().SingleOrDefault(user => user.UserName.ToLower() == userName.ToLower());
        }
    }
}
