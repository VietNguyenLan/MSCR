using Project.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Project.AdminController
{
    public class FeedbackController : Controller
    {
        // GET: Feedback
        public ActionResult Index()
        {
            using (OrderSystemEntities1 db = new OrderSystemEntities1())
            {
                return View(db.feed_back.Include(c => c.user).Include(d => d.order).ToList());
            }
        }

        // GET: Feedback/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Feedback/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Feedback/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Feedback/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Feedback/Edit/5
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

        // GET: Feedback/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Feedback/Delete/5
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
