using Project.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.AdminController
{
    public class IngredientsController : Controller
    {
        // GET: Ingredients
        public ActionResult Index()
        {
            using (OrderSystemEntities1 db = new OrderSystemEntities1())
            {
                return View(db.ingredients.ToList());
            }
        }

        // GET: Ingredients/Details/5
        public ActionResult Details(int id)
        {
            using (OrderSystemEntities1 db = new OrderSystemEntities1())
            {
                 return View(db.ingredients.Where(x => x.id == id).FirstOrDefault());
            }
        }

        // GET: Ingredients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ingredients/Create
        [HttpPost]
        public ActionResult Create(ingredient ingredient)
        {
            try
            {
                using (OrderSystemEntities1 db = new OrderSystemEntities1())
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

        // GET: Ingredients/Edit/5
        public ActionResult Edit(int id)
        {
            using (OrderSystemEntities1 db = new OrderSystemEntities1())
            {

                return View(db.ingredients.Where(x => x.id == id).FirstOrDefault());
            }
        }

        // POST: Ingredients/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ingredient ingredient)
        {
            try
            {
                using (OrderSystemEntities1 db = new OrderSystemEntities1())
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

        // GET: Ingredients/Delete/5
        public ActionResult Delete(int id)
        {
            using (OrderSystemEntities1 db = new OrderSystemEntities1())
            {

                return View(db.ingredients.Where(x => x.id == id).FirstOrDefault());
            }
        }

        // POST: Ingredients/Delete/5
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
