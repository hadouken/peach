using FluentNHibernate.Mapping;
using Peach.Data.Domain;

namespace Peach.Data.Sql.Mappings
{
    public class PluginMap : ClassMap<Plugin>
    {
        public PluginMap()
        {
            Table("Plugins");

            Id(x => x.Id);

            Map(x => x.Description);
            Map(x => x.Homepage);
            Map(x => x.Name);
            Map(x => x.Slug);

            References(x => x.Author).Column("User_Id");

            HasMany(x => x.Releases).Cascade.All();
        }
    }
}
