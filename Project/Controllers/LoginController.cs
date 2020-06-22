using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using
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
                    user.
                }
            }

            return;
        }

    }
}