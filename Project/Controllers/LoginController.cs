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
            using (OrderSystemEntities2 od = new OrderSystemEntities2())
            {
               
                var userDetails = od.users.Where(x => x.username == user.username && x.password == user.password).FirstOrDefault();
                
                if(user.username == "" || user.password == "")
                {
                    return View("Index", user);
                }              
                else if(userDetails == null)
                {
                    user.LoginErrorMsg = "Invalid username or password";
                    return View("Index", user);
                }
                else
                {
                    if (userDetails.role.Equals(2))
                    {
                        return RedirectToAction("Index", "Product");
                    }
                    else
                    {
                        Session["id"] = userDetails.id;
                        Session["username"] = userDetails.username;
                        Session["user"] = userDetails;
                        return RedirectToAction("Home", "Home");
                    }
                }
            }
        }

        public ActionResult LogOut()
        {
            int userID = (int)Session["id"];
            Session.Abandon();
            return RedirectToAction("Home", "Home");
        }
    
    }
}