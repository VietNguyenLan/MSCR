using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.EF;

namespace Project.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        OrderSystemEntities1 db = new OrderSystemEntities1();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(user user)
        {
            var userdetail = db.users.Select(x => x).FirstOrDefault();

            if (ModelState.IsValid && userdetail.username == user.username)
            {
                user.LoginErrorMsg = "user is alreday exist";
                //db.users.Add(user);
                //db.SaveChanges();
                return View("Index", user);

            }
            return View("Index", user);
        }
    }


}