using FluentMigrator;
using FluentMigrator.Runner.Extensions;

namespace Peach.Data.Sql.Migrations
{
    [Migration(20131119122145)]
    public class CreateUsersTable001 : Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsInt32().Identity().NotNullable()
                .WithColumn("ClaimedIdentifier").AsString(400).NotNullable().Unique()
                .WithColumn("UserName").AsString(100).NotNullable().Unique();

            Create.PrimaryKey("PK_Users_Id").OnTable("Users").Column("Id").Clustered();
        }

        public override void Down()
        {
            Delete.Table("Users");
        }
    }
}
