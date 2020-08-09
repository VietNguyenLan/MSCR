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

        public ActionResult CreateMenuDetail()
        {
            SetViewBag();
            SetViewBagMenu();
            return View();
        }

        [HttpPost]
        public ActionResult CreateMenuDetail(menu_detail menu_Detail)
        {
            try
            {
                using (OrderSystemEntities1 db = new OrderSystemEntities1())
                {

                    db.menu_detail.Add(menu_Detail);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public void SetViewBag(long? productID = null)
        {
            ViewBag.productID = new SelectList(ListAll(), "id", "name", productID);
        }

        public void SetViewBagMenu(long? menuId = null)
        {
            ViewBag.menuId = new SelectList(ListAllMenu() ,"id","menu_name",menuId);
        }
        public List<product> ListAll()
        {
            return db.products.Where(x => x.disable == false).ToList();
        }
        public List<menu> ListAllMenu()
        {
            return db.menus.Where(x => x.disable == false).ToList();
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
                    menu.date_create = DateTime.Now;
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
            using (OrderSystemEntities1 db = new OrderSystemEntities1())
            {

                return View(db.menus.Include(m => m.user).Where(x => x.id == id).FirstOrDefault());
            }
        }

        // POST: Menu/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (OrderSystemEntities1 db = new OrderSystemEntities1())
                {

                    menu menu = db.menus.Where(x => x.id == id).FirstOrDefault();
                    db.menus.Remove(menu);
                    db.SaveChanges();

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
