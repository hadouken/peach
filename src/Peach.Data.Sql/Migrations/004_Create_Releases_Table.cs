using FluentMigrator;

namespace Peach.Data.Sql.Migrations
{
    [Migration(20140214222315)]
    public class CreateReleasesTable004 : Migration
    {
        public override void Up()
        {
            Create.Table("Releases")
                .WithColumn("Id").AsInt32().Identity().NotNullable().PrimaryKey()
                .WithColumn("DownloadUri").AsString(500).NotNullable()
                .WithColumn("ReleaseDate").AsDateTime().NotNullable()
                .WithColumn("Version").AsString(100).NotNullable().Unique();
        }

        public override void Down()
        {
            Delete.Table("Releases");
        }
    }
}
