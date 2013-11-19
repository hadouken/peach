using NHibernate;
using Peach.Data.Domain;

namespace Peach.Data.Sql.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ISession session) : base(session)
        {
        }
    }
}
