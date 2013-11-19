using FluentNHibernate.Mapping;
using Peach.Data.Domain;

namespace Peach.Data.Sql.Mappings
{
    public class BlogPostMap : ClassMap<BlogPost>
    {
        public BlogPostMap()
        {
            Table("BlogPosts");

            Id(x => x.Id);

            Map(x => x.Content).Not.Nullable();
            Map(x => x.PublishedDate).Not.Nullable();
            Map(x => x.Slug).Not.Nullable();
            Map(x => x.Title).Length(100).Not.Nullable();

            References(x => x.User).Column("User_Id");
        }
    }
}
