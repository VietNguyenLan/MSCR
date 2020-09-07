using Project.EF;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Project.Controllers
{
    public class OrderDetailController : Controller
    {
        // GET: OrderDetail
        public ActionResult Index(int oID)
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                order order = db.orders.Where(x => x.id == oID).FirstOrDefault();
                ViewBag.orderID = oID;
                ViewBag.code = order.receive_code;
                ViewBag.takeDate = order.take_date;
                var time = "";
                if(order.take_time == 1)
                {
                    time = "6h - 9h";
                }else if (order.take_time == 2)
                {
                    time = "9h - 13h";
                }
                else
                {
                    time = "16h - 19h";
                }
                ViewBag.takeTime = time;
                ViewBag.total = db.order_detail.Where(t => t.orderID == oID).Select(i => i.total_price).Sum();
                List<order_detail> _Details = new List<order_detail>();
                _Details = db.order_detail.Include(o => o.order).Include(a => a.product).Where(x => x.orderID == oID ).ToList();
                return View(_Details);
            }
        }
    }
}