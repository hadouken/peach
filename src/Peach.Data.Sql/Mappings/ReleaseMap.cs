using FluentNHibernate.Mapping;
using Peach.Data.Domain;

namespace Peach.Data.Sql.Mappings
{
    public class ReleaseMap : ClassMap<Release>
    {
        public ReleaseMap()
        {
            Table("Releases");

            Id(x => x.Id);

            Map(x => x.ReleaseDate).Not.Nullable();
            Map(x => x.ReleaseNotes).Not.Nullable();
            Map(x => x.Version).Length(100).Not.Nullable();

            HasMany(x => x.Files).Cascade.AllDeleteOrphan();
        }
    }
}
