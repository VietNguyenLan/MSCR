using Project.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.Entity;

namespace Project.AdminController
{
    public class MenuDailyController : Controller
    {
        OrderSystemEntities1 db = new OrderSystemEntities1();
        // GET: MenuDaily
        public ActionResult Index()
        {
            using (OrderSystemEntities1 db = new OrderSystemEntities1())
            {
                return View(db.time_menu.Include(c => c.menu).ToList());
            }
        }

        // GET: MenuDaily/Details/5
        public ActionResult Details(DateTime date_service)
        {
            using (OrderSystemEntities1 db = new OrderSystemEntities1())
            {
                return View(db.time_menu.Include(c => c.menu).Where(x => x.date_service == date_service).FirstOrDefault());
            }
        }

        // GET: MenuDaily/Create
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        // POST: MenuDaily/Create
        [HttpPost]
        public ActionResult Create(time_menu time_Menu)
        {
            try
            {
                using (OrderSystemEntities1 db = new OrderSystemEntities1())
                {
                    db.time_menu.Add(time_Menu);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public void SetViewBag(long? breakfast_mId = null)
        {
            ViewBag.breakfast_mId = new SelectList(ListAll(), "id", "menu_name", breakfast_mId);
        }
        public List<menu> ListAll()
        {
            return db.menus.Where(x => x.disable == false).ToList();
        }

        // GET: MenuDaily/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MenuDaily/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MenuDaily/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MenuDaily/Delete/5
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
