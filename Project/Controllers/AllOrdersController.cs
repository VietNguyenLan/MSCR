using Project.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Project.Controllers
{
    public class OrderListController : Controller
    {
        // GET: OrderList
        public ActionResult Index(int? page)
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                if (page == null) page = 1;
                int pageSize = 10;

                int uid = (Int32)(Session["id"]);
                var od = db.orders.Where(x => x.userID == uid).ToList().OrderByDescending(a => a.create_time);
                int pageNumber = (page ?? 1);
                return View(od.ToPagedList(pageNumber, pageSize));
            }
        }
    }
}
