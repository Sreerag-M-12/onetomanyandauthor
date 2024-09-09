using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuthorBookDetails.Data;
using AuthorBookDetails.Models;
using NHibernate.Linq;

namespace AuthorBookDetails.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Author
        public ActionResult Index()
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var authors = session.Query<Author>().FetchMany(e => e.Books).ToList();
                return View(authors);
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Author author)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    author.AuthorDetail.Author = author;
                    session.Save(author);
                    txn.Commit();
                }
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int authId)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var author = session.Query<Author>().FirstOrDefault(s => s.Id == authId);
                return View(author);
            }
        }
        [HttpPost]
        public ActionResult Edit(Author author)
        {
            if (ModelState.IsValid)
            {
                using (var session = NHibernateHelper.CreateSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        author.AuthorDetail.Author = author;
                        session.Update(author);
                        transaction.Commit();
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }

        public ActionResult Delete(int authId)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var student = session.Get<Author>(authId);
                return View(student);
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteProduct(int authId)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var author = session.Get<Author>(authId);
                    session.Delete(author);
                    transaction.Commit();
                    return RedirectToAction("Index");
                }
            }
        }
    }
}