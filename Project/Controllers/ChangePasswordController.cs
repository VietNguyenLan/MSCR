using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.ViewModels;
using Project.EF;
using System.Data.Entity;

namespace Project.Controllers
{
    public class ChangePasswordController : Controller
    {
        // GET: ChangePassword
        OrderSystemEntities2 od = new OrderSystemEntities2();
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(ChangePasswordViewModels changePasswordViewModels)
        {
            int uid = (Int32)(Session["id"]);
            var userid = od.users.Find(uid);
            if (userid.password == changePasswordViewModels.OldPassword)
            {
                userid.password = changePasswordViewModels.NewPassword;
                od.Entry(userid).State = EntityState.Modified;
                od.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else if (userid.password != changePasswordViewModels.NewPassword)
            {
                changePasswordViewModels.ErrorMsg = "Old Password is not correct !";
                return View("Index", changePasswordViewModels);
            }
            else
            {
                return View("Index", changePasswordViewModels);
            }
        }



         
    }
}