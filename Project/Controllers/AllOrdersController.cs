using Project.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class OrderListController : Controller
    {
        // GET: OrderList
        public ActionResult Index()
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {
               
                int uid = (Int32)(Session["id"]);
               
                return View(db.orders.Where(x => x.userID == uid).ToList());
            }
        }
    }
}
