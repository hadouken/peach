using FluentMigrator;

namespace Peach.Data.Sql.Migrations
{
    [Migration(20131119153513)]
    public class CreateBlogPostsTable002 : Migration
    {
        public override void Up()
        {
            Create.Table("BlogPosts")
                .WithColumn("Id").AsInt32().Identity().NotNullable().PrimaryKey()
                .WithColumn("User_Id").AsInt32().NotNullable()
                .WithColumn("Title").AsString(100).NotNullable()
                .WithColumn("Slug").AsString(100).NotNullable()
                .WithColumn("PublishedDate").AsDateTime().NotNullable()
                .WithColumn("Content").AsString(int.MaxValue).NotNullable();

            Create.ForeignKey("FK_BlogPosts_Users")
                .FromTable("BlogPosts")
                .ForeignColumn("User_Id")
                .ToTable("Users")
                .PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table("BlogPosts");
        }
    }
}
