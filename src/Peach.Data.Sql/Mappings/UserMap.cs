using FluentNHibernate.Mapping;
using Peach.Data.Domain;

namespace Peach.Data.Sql.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);
            Map(x => x.ClaimedIdentifier).Not.Nullable().Length(400);
            Map(x => x.UserName).Not.Nullable().Length(100);
        }
    }
}
