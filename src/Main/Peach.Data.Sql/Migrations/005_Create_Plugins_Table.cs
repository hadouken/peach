using FluentMigrator;

namespace Peach.Data.Sql.Migrations
{
    [Migration(20140222160017)]
    public class CreatePluginsTable005 : Migration
    {
        public override void Up()
        {
            Create.Table("Plugins")
                .WithColumn("Id").AsInt32().Identity().NotNullable().PrimaryKey()
                .WithColumn("User_Id").AsInt32().NotNullable()
                .WithColumn("Name").AsString(200).NotNullable().Unique()
                .WithColumn("Slug").AsString(200).NotNullable().Unique()
                .WithColumn("Homepage").AsString(255)
                .WithColumn("Description").AsString(int.MaxValue);

            Create.Table("PluginReleases")
                .WithColumn("Id").AsInt32().Identity().NotNullable().PrimaryKey()
                .WithColumn("Plugin_Id").AsInt32().NotNullable()
                .WithColumn("Version").AsString(10).NotNullable()
                .WithColumn("DownloadUri").AsString(255).NotNullable()
                .WithColumn("ReleaseDate").AsDateTime().NotNullable()
                .WithColumn("ReleaseNotes").AsString(int.MaxValue);

            Create.ForeignKey("FK_PluginReleases_Plugins")
                .FromTable("PluginReleases")
                .ForeignColumn("Plugin_Id")
                .ToTable("Plugins")
                .PrimaryColumn("Id");

        }

        public override void Down()
        {
            Delete.Table("PluginReleases");
            Delete.Table("Plugins");
        }
    }
}
