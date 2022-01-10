using BookStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Repostories
{
    public class BookStoreRepostory : IBookStoreRepostory<Book>
    {
        List<Book> books;
        public BookStoreRepostory()
        {
            books = new List<Book>()
            {
                new Book(){id=1, Title = "C# programming",Descryption="No Descryption" ,Auther=new Auther(){id=1 } },
                new Book(){id=2, Title = "Java programming",Descryption="No Thing" ,Auther=new Auther(){id=1 }},
                new Book(){id=3, Title = "Pyson programming",Descryption="No Data" ,Auther=new Auther(){id=3 }},

            };

        }
        public void Add(Book entity)
        {
            if (books.Count == 0)
            {
                entity.id = 1;
                books.Add(entity);


            }
            else
            {
                entity.id = books.Max(x => x.id) + 1;
                books.Add(entity);
            }

            
            

        }

        public void Delete(int id)
        {
            var book = Find(id);
            books.Remove(book);
        }

        public Book Find(int id)
        {
            var book = books.SingleOrDefault(b=>b.id==id);
            return book;
        }

        public IList<Book> GetAll()
        {
            return books;
        }

        public IList<Book> Search(string trm)
        {
            throw new System.NotImplementedException();
        }

        public void Update(int id,Book entity)
        {
            var book = Find(id);
            if (entity != null)
            {
                book.Title = entity.Title;
                book.Descryption = entity.Descryption;
                book.Auther = entity.Auther;
            }
        }
    }
}
