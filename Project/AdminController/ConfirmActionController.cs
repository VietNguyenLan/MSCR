using Project.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.AdminController
{
    public class ConfirmActionController : Controller
    {
        // GET: ConfirmAction
        public ActionResult Index()
        {
            using(OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                int uID = (Int32)(Session["id"]);
                Random rnd = new Random();
                int otp = rnd.Next(1000, 10000);
                otp_table otp_ = new otp_table()
                {
                    uId = uID,
                    create_time = DateTime.Now,
                    otp = otp
                };
                db.otp_table.Add(otp_);
                db.SaveChanges();
                return View(otp);
            }
            
        }
    }
}