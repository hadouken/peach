namespace Peach.Core.Data
{
    public interface IRepository<T>
    {
        void Insert(T item);
        void Update(T item);
        void Delete(T item);

        T GetById(object id);
    }
}
