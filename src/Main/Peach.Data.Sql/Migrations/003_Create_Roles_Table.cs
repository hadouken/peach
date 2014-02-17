using FluentMigrator;
using Peach.Data.Domain;

namespace Peach.Data.Sql.Migrations
{
    [Migration(20131126093724)]
    public class CreateRolesTable003 : Migration
    {
        public override void Up()
        {
            Create.Table("Roles")
                .WithColumn("Id").AsInt32().Identity().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString(100).NotNullable();

            Create.Table("Users_Roles")
                .WithColumn("Id").AsInt32().Identity().NotNullable().PrimaryKey()
                .WithColumn("User_Id").AsInt32().NotNullable()
                .WithColumn("Role_Id").AsInt32().NotNullable();

            Create.ForeignKey()
                .FromTable("Users_Roles")
                .ForeignColumn("User_Id")
                .ToTable("Users")
                .PrimaryColumn("Id");

            Create.ForeignKey()
                .FromTable("Users_Roles")
                .ForeignColumn("Role_Id")
                .ToTable("Roles")
                .PrimaryColumn("Id");

            Insert.IntoTable("Roles")
                .Row(new {Name = Role.Administrator})
                .Row(new {Name = Role.ContentWriter})
                .Row(new {Name = Role.PluginDeveloper});
        }

        public override void Down()
        {
            Delete.Table("Users_Roles");
            Delete.Table("Roles");
        }
    }
}
