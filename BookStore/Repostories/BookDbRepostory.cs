using BookStore.Data;
using BookStore.Models;
using BookStore.Repostories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
namespace BookStore.Repostories

{
    public class BookDbRepostory : IBookStoreRepostory<Book>
    {
    BookStoreDbContext db;
    public BookDbRepostory(BookStoreDbContext _db)
    {
        db = _db;
    }
    public void Add(Book entity)
        {
          
                db.Books.Add(entity);
                db.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = Find(id);
            if (book != null)
            {
                db.Books.Remove(book);
                db.SaveChanges();
            }
           

        }

        public Book Find(int id)
        {
            var book = db.Books.Include(a => a.Auther).SingleOrDefault(b => b.id == id);
            return book;
        }

        public IList<Book> GetAll()
        {
            return db.Books.Include(a=>a.Auther).ToList();
        }

        public void Update(int id, Book entity)
        {
            db.Update(entity);
            db.SaveChanges();

        }
        public IList<Book> Search(string trm)
        {
            var result = db.Books.Include(a => a.Auther).Where(b => b.Title.Contains(trm) 
            || b.Descryption.Contains(trm)||b.Auther.FullName.Contains(trm));
            return result.ToList();
        }
    }
}

