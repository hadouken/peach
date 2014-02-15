using FluentNHibernate.Mapping;
using Peach.Data.Domain;

namespace Peach.Data.Sql.Mappings
{
    public class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Table("Roles");

            Id(x => x.Id);
            Map(x => x.Name).Length(100).Not.Nullable();
        }
    }
}
