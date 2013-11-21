using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Linq;
using Peach.Core;
using Peach.Core.Data;

namespace Peach.Data.Sql.Repositories
{
    public class Repository<T> : IRepository<T>
    {
        private readonly ISession _session;

        public Repository(ISession session)
        {
            _session = session;
        }

        protected ISession Session
        {
            get { return _session; }
        }

        public void Insert(T item)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Save(item);
                transaction.Commit();
            }
        }

        public void Update(T item)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Update(item);
                transaction.Commit();
            }
        }

        public void Delete(T item)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Delete(item);
                transaction.Commit();
            }
        }

        public T GetById(object id)
        {
            return _session.Get<T>(id);
        }

        public int Count()
        {
            return _session.Query<T>().Count();
        }

        public int Count(Expression<Func<T, bool>> query)
        {
            return _session.Query<T>().Where(query).Count();
        }

        public IEnumerable<T> GetAll()
        {
            return _session.Query<T>().ToArray();
        }

        public IEnumerable<T> GetPage<TProperty>(
            Func<T, TProperty> sorter,
            SortOrder sortOrder = SortOrder.Ascending,
            int page = 0,
            int pageSize = 10)
        {
            if (pageSize == 0)
            {
                return Enumerable.Empty<T>();
            }

            var query = Session.Query<T>();
            var result = sortOrder == SortOrder.Ascending ? query.OrderBy(sorter) : query.OrderByDescending(sorter);
            return result.Skip(page*pageSize).Take(pageSize);
        }
        
    }
}
