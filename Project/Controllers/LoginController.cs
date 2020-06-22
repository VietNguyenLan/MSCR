using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.EF;

namespace Project.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authourise(user user)
        {
            using (OrderSystemEntities od = new OrderSystemEntities())
            {
                var userDetails = od.users.Where(x => x.username == user.username && x.password == user.password).FirstOrDefault();
                if(userDetails == null)
                {
                    user.LoginErrorMsg = "Invalid Username or password";
                    return View("Index", user);
                }
                else
                {
                    Session["id"] = user.id;
                    Session["username"] = user.username;
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public ActionResult LogOut()
        {
            int userID = (int)Session["id"];
            Session.Abandon();
            return RedirectToAction("Index, Login");
        }
    }
}