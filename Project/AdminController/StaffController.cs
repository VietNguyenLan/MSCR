using Project.EF;
using Project.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.AdminController
{
    public class StaffController : Controller
    {
        // GET: Staff
        [DeatAuthorize(Order = 3)]
        public ActionResult Index()
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                return View(db.users.Where(x => x.role == 2).ToList());
            }
        }

       

        // GET: Staff/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Staff/Create
        [HttpPost]
        public ActionResult Create(user user,FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                OrderSystemEntities2 db = new OrderSystemEntities2();
                user u = new user();

                u.name = user.name;
                u.username = user.username;
                u.password = user.password;
                u.address = user.address;
                u.phone_num = user.phone_num;
                u.email = user.email;
                u.role = 2;
                u.avt_img = "123";
                u.is_active = true;
                u.email_verified = true;

                db.users.Add(u);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Staff/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Staff/Edit/5
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

        // GET: Staff/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Staff/Delete/5
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
