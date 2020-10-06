using PagedList;
using Project.EF;
using Project.Security;
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
        OrderSystemEntities2 db = new OrderSystemEntities2();
        [DeatAuthorize(Order = 3)]
        public ActionResult Index(int? page)
        {

            if (page == null) page = 1;
            int pageSize = 5;
            var cat = db.categories.ToList().OrderBy(x => x.id);

            int pageNumber = (page ?? 1);
            return View(cat.ToPagedList(pageNumber, pageSize));

        }

        // GET: Category/Details/5
        [DeatAuthorize(Order = 3)]
        public ActionResult Details(int id)
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {

                return View(db.categories.Where(x => x.id == id).FirstOrDefault());
            }
        }

        // GET: Category/Create
        [DeatAuthorize(Order = 3)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [DeatAuthorize(Order = 3)]
        [HttpPost]
        public ActionResult Create(category category)
        {
            try
            {
                using(OrderSystemEntities2 db = new OrderSystemEntities2())
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
        [DeatAuthorize(Order = 3)]
        public ActionResult Edit(int id)
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {
               
                return View(db.categories.Where(x => x.id == id).FirstOrDefault());
            }
        }

        // POST: Category/Edit/5
        [DeatAuthorize(Order = 3)]
        [HttpPost]
        public ActionResult Edit(int id, category category)
        {
            try
            {
                // TODO: Add update logic here
                using (OrderSystemEntities2 db = new OrderSystemEntities2())
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
        [DeatAuthorize(Order = 3)]
        public ActionResult Delete(int id)
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {

                return View(db.categories.Where(x => x.id == id).FirstOrDefault());
            }
        }

        // POST: Category/Delete/5
        [DeatAuthorize(Order = 3)]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (OrderSystemEntities2 db = new OrderSystemEntities2())
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
