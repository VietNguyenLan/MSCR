using Project.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.AdminController
{
    public class ProductController : Controller
    {
        OrderSystemEntities1 db = new OrderSystemEntities1();
        // GET: Product
        public ActionResult Index()
        {
            using (OrderSystemEntities1 db = new OrderSystemEntities1())
            {
                return View(db.products.ToList());
            }
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
       

        // GET: Product/Create
        public ActionResult Create()
        {
            try
            {
                using(OrderSystemEntities1 db = new OrderSystemEntities1())
                {

                    SetViewBag();


                
                }
            }
            catch
            {

            }
            return View();
        }

        public void SetViewBag(long? categoryID = null )
        {
            ViewBag.categoryID = new SelectList(ListAll(), "id", "name" , categoryID);
        }
        public List<category> ListAll()
        {
            return db.categories.Where(x => x.disable == true).ToList();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(product product)
        {
            try
            {
                // TODO: Add insert logic here
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
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

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
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
