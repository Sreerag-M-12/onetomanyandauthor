using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentOneToMany.Data;
using FluentOneToMany.Models;
using NHibernate.Linq;

namespace FluentOneToMany.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var employees = session.Query<Employee>().FetchMany(e=>e.Orders).ToList();
                return View(employees);
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    
                    session.Save(employee);
                    txn.Commit();
                }
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int id)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var employee = session.Query<Employee>().FirstOrDefault(e => e.Id == id);
                return View(employee);
            }
        }
        [HttpPost]
        public ActionResult Edit(Employee employee)
        {

            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    session.Update(employee);
                    txn.Commit();
                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult Delete(int id)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var employee = session.Query<Employee>().FirstOrDefault(e => e.Id == id);
                return View(employee);
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteOrder(int id)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    var employee = session.Get<Employee>(id);
                    session.Delete(employee);
                    txn.Commit();
                    return RedirectToAction("Index");
                }
            }
        }
    }
}