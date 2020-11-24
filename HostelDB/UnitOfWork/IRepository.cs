using System.Collections.Generic;

namespace HostelDB.Repositories
{
    interface IRepository<T>
        where T : class
    {
        IEnumerable<T> GetObjectList();
        T GetObject(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
