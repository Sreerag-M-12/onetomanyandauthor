using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentOneToMany.Data;
using FluentOneToMany.Models;
using NHibernate.Criterion;
using Order = FluentOneToMany.Models.Order;

namespace FluentOneToMany.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OrderDetails(int empid)
        {
            Session["empid"]=empid;
            using (var session = NHibernateHelper.CreateSession())
            {

                var orders = session.Query<Order>().Where(o => o.Employee.Id == empid).ToList();
                return View(orders);
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Order order)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var empid = Session["empId"];
                using (var txn = session.BeginTransaction())
                {
                    var employee = session.Query<Employee>().FirstOrDefault(e=>e.Id == (int)empid);
                    
                        order.Employee = employee;
                        session.Save(order);
                        txn.Commit();
                        return RedirectToAction("Index","Employee");
                    
                }
            }
        }

        public ActionResult Edit(int id)

        {

            using (var session = NHibernateHelper.CreateSession())

            {

                //var val = Session["empId"];

                //var employee = session.Query<Employee>().FirstOrDefault(e => e.Id == (int)val);

                var targetOrder = session.Get<Order>(id);

                return View(targetOrder);

            }

        }


        [HttpPost]

        public ActionResult Edit(Order order)

        {
            using (var session = NHibernateHelper.CreateSession())
            {

                using (var txn = session.BeginTransaction())
                {

                    var existingOrder = session.Get<Order>(order.Id);

                    // existingOrder.Employee = employee;                                 //no need for this statement as it is already existing so linkage is established

                    existingOrder.Description = order.Description;

                    session.Update(existingOrder);

                    txn.Commit();

                    return RedirectToAction("Index", "Employee");

                }

            }

        }

        public ActionResult Delete(int id)
        {

            using (var session = NHibernateHelper.CreateSession())

            {

                var empId = Session["empId"];

                var employee = session.Query<Employee>().FirstOrDefault(e => e.Id == (int)empId);

                var targetOrder = employee.Orders.FirstOrDefault(o => o.Id == id);

                return View(targetOrder);

            }

        }

        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteOrder(int orderId)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    var targetOrder = session.Get<Order>(orderId);

                    session.Delete(targetOrder);

                    txn.Commit();

                    return RedirectToAction("Index", "Employee");
                }
            }
        }
    }
}