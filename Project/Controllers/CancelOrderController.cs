using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.EF;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class CancelOrderController : Controller
    {
        // GET: CancelOrder
        public ActionResult Index(order o)
        {
            int uID = (Int32)(Session["id"]);
            using(OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                o.is_cancle = true;
                db.SaveChanges();
                ViewBag.notice = "Order no: " + o.id + "cancle successed";
                return View();
            }
            
        }
    }
}