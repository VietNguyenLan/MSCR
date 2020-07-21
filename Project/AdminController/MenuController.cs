using Project.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.AdminController
{
    public class MenuController : Controller
    {

        OrderSystemEntities1 db = new OrderSystemEntities1();
        // GET: Menu
        public ActionResult Index()
        {
            using (OrderSystemEntities1 db = new OrderSystemEntities1())
            {
                return View(db.menus.Include(a => a.user).ToList());
            }
        }

        // GET: Menu/Details/5
        public ActionResult Details(int id)
        {
            using (OrderSystemEntities1 db = new OrderSystemEntities1())
            {
                SetViewBag();
                return View(db.menu_detail.Include(c => c.product).Include(a => a.menu).Where(x => x.menuId == id).ToList());
                
            }
        }

        public void SetViewBag(long? productID = null)
        {
            ViewBag.productID = new SelectList(ListAll(), "id", "name", productID);
        }
        public List<product> ListAll()
        {
            return db.products.Where(x => x.disable == false).ToList();
        }


        // GET: Menu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Menu/Create
        [HttpPost]
        public ActionResult Create(menu menu)
        {
            try
            {
                using (OrderSystemEntities1 db = new OrderSystemEntities1())
                {
                    db.menus.Add(menu);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Menu/Edit/5
        public ActionResult Edit(int id)
        {
            using (OrderSystemEntities1 db = new OrderSystemEntities1())
            {

                return View(db.menus.Where(x => x.id == id).FirstOrDefault());
            }
        }

        // POST: Menu/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, menu menu)
        {
            try
            {
                 using (OrderSystemEntities1 db = new OrderSystemEntities1())
                {
                    db.Entry(menu).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Menu/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Menu/Delete/5
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
