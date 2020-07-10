using Project.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.AdminController
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            using (OrderSystemEntities1 db = new OrderSystemEntities1())
            {
                return View(db.categories.ToList());
            }
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            using (OrderSystemEntities1 db = new OrderSystemEntities1())
            {

                return View(db.categories.Where(x => x.id == id).FirstOrDefault());
            }
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(category category)
        {
            try
            {
                using(OrderSystemEntities1 db = new OrderSystemEntities1())
                {
                    db.categories.Add(category);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            using (OrderSystemEntities1 db = new OrderSystemEntities1())
            {
               
                return View(db.categories.Where(x => x.id == id).FirstOrDefault());
            }
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, category category)
        {
            try
            {
                // TODO: Add update logic here
                using (OrderSystemEntities1 db = new OrderSystemEntities1())
                {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
                }
                   
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            using (OrderSystemEntities1 db = new OrderSystemEntities1())
            {

                return View(db.categories.Where(x => x.id == id).FirstOrDefault());
            }
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (OrderSystemEntities1 db = new OrderSystemEntities1())
                {

                    category category = db.categories.Where(x => x.id == id).FirstOrDefault();
                    db.categories.Remove(category);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
