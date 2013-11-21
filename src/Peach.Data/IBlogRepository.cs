using Peach.Core.Data;
using Peach.Data.Domain;

namespace Peach.Data
{
    public interface IBlogRepository : IRepository<BlogPost>
    {
        BlogPost GetByYearMonthAndSlug(int year, int month, string slug);
    }
}
