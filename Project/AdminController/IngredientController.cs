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
    public class IngredientController : Controller
    {
        // GET: Ingredient
         OrderSystemEntities2 db = new OrderSystemEntities2();
        [DeatAuthorize(Order = 3)]
        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            int pageSize = 5;


            int pageNumber = (page ?? 1);
            var ingre = db.ingredients.ToList().OrderBy(a => a.id);
            return View(ingre.ToPagedList(pageNumber, pageSize));

        }

        // GET: Ingredient/Details/5
        public ActionResult Details(int id)
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                return View(db.ingredients.Where(x => x.id == id).FirstOrDefault());
            }
        }

        // GET: Ingredient/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ingredient/Create
        [HttpPost]
        public ActionResult Create(ingredient ingredient)
        {
            try
            {
                using (OrderSystemEntities2 db = new OrderSystemEntities2())
                {
                    db.ingredients.Add(ingredient);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ingredient/Edit/5
        public ActionResult Edit(int id)
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {

                return View(db.ingredients.Where(x => x.id == id).FirstOrDefault());
            }
        }

        // POST: Ingredient/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ingredient ingredient)
        {
            try
            {
                using (OrderSystemEntities2 db = new OrderSystemEntities2())
                {
                    db.Entry(ingredient).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Ingredient/Delete/5
        public ActionResult Delete(int id)
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {

                return View(db.ingredients.Where(x => x.id == id).FirstOrDefault());
            }
        }

        // POST: Ingredient/Delete/5
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
