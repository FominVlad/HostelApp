using System.Collections.Generic;

namespace HostelDB.Repositories
{
    interface IRepository<T>
        where T : class
    {
        IEnumerable<T> GetObjectList();
        T GetObject(int id);
        T Create(T item);
        bool Update(T item);
        bool Delete(int id);
    }
}
