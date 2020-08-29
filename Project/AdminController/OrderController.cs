using Project.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Project.Security;

namespace Project.AdminController
{
    public class OrderController : Controller
    {

        OrderSystemEntities2 db = new OrderSystemEntities2();


        // GET: Order
     
        [DeatAuthorize(Order = 2)]
        
        public ActionResult Index()
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                return View(db.orders.Include(c => c.user).Include(b => b.user1).Include(a => a.service_time).ToList());
            }
        }
       
        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())

            {
                ViewBag.total = db.order_detail.Where(t => t.orderID == id ).Select(i => i.total_price).Sum();
                return View(db.order_detail.Include(c => c.order).Include(d => d.product).Where(x => x.orderID == id).ToList());
            }
        }

        
        // GET: Order/Create
        public ActionResult Create()
        {
            SetViewBagUserID();
            SetViewBagTime(); SetViewBagStaff();
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(order order)
        {
            try
            {
                using (OrderSystemEntities2 db = new OrderSystemEntities2())
                {
                    
                    db.orders.Add(order);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public void SetViewBagUserID(long? userID = null)
        {
            ViewBag.userID = new SelectList(ListAll(), "id", "name", userID);
        }
        public List<user> ListAll()
        {
            return db.users.Where(x => x.role == 1).ToList();
        }

        public void SetViewBagTime(long? take_time = null)
        {
            ViewBag.take_time = new SelectList(ListAllTime(), "id", "time", take_time);
        }
        public List<service_time> ListAllTime()
        {
            return db.service_time.ToList();
        }

        public void SetViewBagStaff(long? staffID = null)
        {
            ViewBag.staffID = new SelectList(ListAllStaff(), "id", "name", staffID);
        }
        public List<user> ListAllStaff()
        {
            return db.users.Where(x => x.role == 2).ToList();
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                SetViewBagUserID();
                SetViewBagTime();
                SetViewBagStaff();
                return View(db.orders.Where(x => x.id == id).FirstOrDefault());
            }
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,order order, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
