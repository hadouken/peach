using FluentNHibernate.Mapping;
using Peach.Data.Domain;

namespace Peach.Data.Sql.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("Users");

            Id(x => x.Id);
            Map(x => x.ClaimedIdentifier).Not.Nullable().Length(400);
            Map(x => x.UserName).Not.Nullable().Length(100);

            // User->Roles
            HasManyToMany(x => x.Roles)
                .Table("Users_Roles")
                .ParentKeyColumn("User_Id")
                .ChildKeyColumn("Role_Id")
                .Cascade.All();
        }
    }
}
