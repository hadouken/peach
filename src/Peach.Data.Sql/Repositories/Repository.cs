using System;
using NHibernate;
using Peach.Core.Data;

namespace Peach.Data.Sql.Repositories
{
    public class Repository<T> : IRepository<T>, IDisposable
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

        public void Dispose()
        {
            _session.Flush();
            _session.Close();
        }
    }
}
