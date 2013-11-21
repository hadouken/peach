using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Peach.Core.Data
{
    public interface IRepository<T>
    {
        void Insert(T item);
        void Update(T item);
        void Delete(T item);

        T GetById(object id);

        int Count();
        int Count(Expression<Func<T, bool>> query);

        IEnumerable<T> GetAll();
        IEnumerable<T> GetPage<TProperty>(Func<T, TProperty> sorter, SortOrder sortOrder = SortOrder.Ascending, int page = 0, int pageSize = 10);
    }
}
