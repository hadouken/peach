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
                .WithColumn("ReleaseDate").AsDateTime().NotNullable()
                .WithColumn("ReleaseNotes").AsString(int.MaxValue).NotNullable().Nullable()
                .WithColumn("Version").AsString(100).NotNullable().Unique();

            Create.Table("ReleaseFiles")
                .WithColumn("Id").AsInt32().Identity().NotNullable().PrimaryKey()
                .WithColumn("Release_Id").AsInt32().NotNullable()
                .WithColumn("DownloadUri").AsString(500).NotNullable();

            Create.ForeignKey("FK_ReleaseFiles_Release")
                .FromTable("ReleaseFiles")
                .ForeignColumn("Release_Id")
                .ToTable("Releases")
                .PrimaryColumn("Id");

        }

        public override void Down()
        {
            Delete.Table("ReleaseFiles");
            Delete.Table("Releases");
        }
    }
}
