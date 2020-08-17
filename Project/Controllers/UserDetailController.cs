using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.EF;

namespace Project.Controllers
{
    public class UserDetailController : Controller
    {
        // GET: UserDetail
        public ActionResult UserDetail()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                using(OrderSystemEntities1 db = new OrderSystemEntities1())
                {
                    int uID = (int)Session["id"];
                    var User = db.users.Where(x => x.id == uID).FirstOrDefault();
                    return View(User);
                }
            }
        }
    }
}