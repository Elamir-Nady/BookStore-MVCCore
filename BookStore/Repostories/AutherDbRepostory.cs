using BookStore.Data;
using BookStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Repostories
{
    public class AutherDbRepostory : IBookStoreRepostory<Auther>
    {
        BookStoreDbContext db;
        public AutherDbRepostory(BookStoreDbContext _db)
        {
            db = _db;
        }
        public void Add(Auther entity)
        {
         
                db.Authers.Add(entity);
                db.SaveChanges();



        }

        public void Delete(int id)
        {
            var Auther = Find(id);
            if (Auther != null)
            {
                db.Authers.Remove(Auther);
                db.SaveChanges();
            }
          

        }

        public Auther Find(int id)
        {
            var auther = db.Authers.SingleOrDefault(a => a.id == id);
            return auther;
        }

        public IList<Auther> GetAll()
        {
            return db.Authers.ToList();
        }

        public IList<Auther> Search(string trm)
        {
            return db.Authers.Where(a=>a.FullName.Contains(trm)).ToList();
        }

        public void Update(int id, Auther entity)
        {
            db.Update(entity);

            db.SaveChanges();
        }
    }
}
