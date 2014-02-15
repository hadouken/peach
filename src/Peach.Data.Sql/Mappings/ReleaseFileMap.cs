using FluentNHibernate.Mapping;
using Peach.Data.Domain;

namespace Peach.Data.Sql.Mappings
{
    public class ReleaseFileMap : ClassMap<ReleaseFile>
    {
        public ReleaseFileMap()
        {
            Table("ReleaseFiles");

            Id(x => x.Id);

            Map(x => x.DownloadUri).Length(500).Not.Nullable();

            References(x => x.Release).Not.Nullable();
        }
    }
}
