using BookStore.Models;
using BookStore.Repostories;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        public IBookStoreRepostory<Book> BookStoreRepostory { get; }
        public IBookStoreRepostory<Auther> AutherRepostory { get; }
        public IHostingEnvironment Hosting { get; }

        public BookController(
            IBookStoreRepostory<Book>  bookStoreRepostory,
            IBookStoreRepostory<Auther> autherRepostory,
            IHostingEnvironment hosting)
        {
            BookStoreRepostory = bookStoreRepostory;
            AutherRepostory = autherRepostory;
            Hosting = hosting;
        }
        // GET: BookController
        public ActionResult Index()
        {
            var books = BookStoreRepostory.GetAll();
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = BookStoreRepostory.Find(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var model = new BookAutherViewModel
            {
                Authers = FillSelectList()
            };
            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAutherViewModel model)
        {
            model.Authers = FillSelectList();

            if (ModelState.IsValid)
            {

            
                try
                {
                    string filename = string.Empty;
                    if (model.File != null)
                    {
                        string uplodes = Path.Combine(Hosting.WebRootPath, "Uplodes");
                        filename = model.File.FileName;
                        //filename = model.File.FileName + (DateTime.Now.ToString());

                        string Fullpath = Path.Combine(uplodes, filename);
                        model.File.CopyTo(new FileStream(Fullpath, FileMode.Create));
                    }
                    if(model.AutherID == -1)
                    {
                        ViewBag.Message = "Please Select an Auther From This List";

                        return View(model);
                    }
                    Book book = new Book
                    {
                        id = model.BookID,
                        Title = model.Title,
                        Descryption = model.Descryption,
                        Auther = AutherRepostory.Find(model.AutherID),
                        ImageURL = filename

                    };
                    BookStoreRepostory.Add(book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "You have to fill all required fileds !");
                return View(model);

            }
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {

            var book = BookStoreRepostory.Find(id);
            var autherID = book.Auther == null ? book.Auther.id = 0 : book.Auther.id;

            var model = new BookAutherViewModel
            {
                BookID = book.id,
                Title = book.Title,
                Descryption = book.Descryption,
                AutherID = autherID,
                Authers = FillSelectList(),
                ImageURL=book.ImageURL
            };
            return View(model);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookAutherViewModel model)
        {
            try
            {
                string filename = string.Empty;
                if (model.File != null)
                {
                    string uplodes = Path.Combine(Hosting.WebRootPath, "Uplodes");
                    filename = model.File.FileName;
                    //filenam e = model.File.FileName + (DateTime.Now.ToString());
                    string oldFilename = model.ImageURL;
                    string fullOldPath= Path.Combine(uplodes, oldFilename);
                    string Fullpath = Path.Combine(uplodes, filename);

                    if (fullOldPath!= Fullpath)
                    {
                        System.IO.File.Delete(fullOldPath);
                        model.File.CopyTo(new FileStream(Fullpath, FileMode.Create));
                    }
                
                }
                Book book = new Book
                {
                    id = model.BookID,
                    Title = model.Title,
                    Descryption = model.Descryption,
                    Auther = AutherRepostory.Find(model.AutherID),
                    ImageURL= filename

                };
                BookStoreRepostory.Update(id, book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = BookStoreRepostory.Find(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Book book)
        {
            try
            {
                BookStoreRepostory.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        List<Auther> FillSelectList()
        {
            var authers = AutherRepostory.GetAll().ToList();
            authers.Insert(0, new Auther
            {
                id = -1,
                FullName = "....Please Select Auther Name...."
            });
            return authers;
        }
        public ActionResult Search(string trm)
        {

            var result = BookStoreRepostory.Search(trm);
          
            return View("Index", result);
        }
    }
}
