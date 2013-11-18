using FluentNHibernate.Mapping;
using Peach.Data.Domain;

namespace Peach.Data.Sql.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);
            Map(x => x.ClaimedIdentifier).Not.Nullable();
            Map(x => x.UserName).Not.Nullable();
        }
    }
}
