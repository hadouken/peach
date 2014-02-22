using FluentNHibernate.Mapping;
using Peach.Data.Domain;

namespace Peach.Data.Sql.Mappings
{
    public class PluginReleaseMap : ClassMap<PluginRelease>
    {
        public PluginReleaseMap()
        {
            Table("PluginReleases");

            Id(x => x.Id);

            Map(x => x.DownloadUri).Not.Nullable();
            Map(x => x.ReleaseDate);
            Map(x => x.ReleaseNotes);
            Map(x => x.Version);

            References(x => x.Plugin);
        }
    }
}
