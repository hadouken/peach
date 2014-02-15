using FluentMigrator;

namespace Peach.Data.Sql.Migrations
{
    [Migration(20131119122145)]
    public class CreateUsersTable001 : Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsInt32().Identity().NotNullable().PrimaryKey()
                .WithColumn("ClaimedIdentifier").AsString(400).NotNullable().Unique()
                .WithColumn("UserName").AsString(100).NotNullable().Unique();
        }

        public override void Down()
        {
            Delete.Table("Users");
        }
    }
}
