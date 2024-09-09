using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuthorBookDetails.Data;
using AuthorBookDetails.Models;
using NHibernate.Criterion;

namespace AuthorBookDetails.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(int authId)
        {
            Session["authid"] = authId;
            using (var session = NHibernateHelper.CreateSession())
            {

                var books = session.Query<Book>().Where(a=>a.Author.Id ==authId).ToList();
                return View(books);
            }
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Book book)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var authId = Session["authId"];
                using (var txn = session.BeginTransaction())
                {
                    var author = session.Query<Author>().FirstOrDefault(e => e.Id == (int)authId);
                    book.Author = author;
                    session.Save(book);
                    txn.Commit();
                    return RedirectToAction("Index","Author");

                }
            }
        }

        public ActionResult Edit(int id)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var targetBook = session.Get<Book>(id);
                return View(targetBook);
            }

        }


        [HttpPost]
        public ActionResult Edit(Book book)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    var existingBook = session.Get<Book>(book.Id);
                    existingBook.Title = book.Title;
                    existingBook.Genre= book.Genre;
                    existingBook.Description = book.Description;
                    session.Update(existingBook);
                    txn.Commit();
                    return RedirectToAction("Index", "Author");
                }

            }

        }

        public ActionResult Delete(int id)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var authId = Session["authId"];
                var author = session.Query<Author>().FirstOrDefault(e => e.Id == (int)authId);
                var targetBook = author.Books.FirstOrDefault(o => o.Id == id);
                return View(targetBook);
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteBook(int id)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    var targetBook = session.Get<Book>(id);

                    session.Delete(targetBook);

                    txn.Commit();

                    return RedirectToAction("Index", "Author");
                }
            }
        }

    }
}