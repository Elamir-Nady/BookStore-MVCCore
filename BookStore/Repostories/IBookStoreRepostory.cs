using BookStore.Models;
using System.Collections.Generic;

namespace BookStore.Repostories
{
    public interface IBookStoreRepostory<T>
    {
        IList<T> GetAll();
        T Find(int id);
        void Add(T entity);
        void Update(int id,T entity);
        void Delete(int id);
        IList<T> Search(string trm);
    }
}
