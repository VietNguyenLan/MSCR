using Project.EF;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Project.StaffControllers
{
    public class CurrentOrderDetailController : Controller
    {
        // GET: CurrentOrderDetail
        public ActionResult Index(int orderID, int recieved_code)
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                order order = db.orders.Where(x => x.id == orderID && x.receive_code == recieved_code).FirstOrDefault();
                ViewBag.orderID = orderID;
                ViewBag.code = order.receive_code;
                ViewBag.takeDate = order.take_date;
                var time = "";
                if (order.take_time == 1)
                {
                    time = "6h - 9h";
                }
                else if (order.take_time == 2)
                {
                    time = "9h - 13h";
                }
                else
                {
                    time = "16h - 19h";
                }
                ViewBag.takeTime = time;
                ViewBag.total = db.order_detail.Where(t => t.orderID == orderID).Select(i => i.total_price).Sum();
                List<order_detail> _Details = new List<order_detail>();
                _Details = db.order_detail.Include(o => o.order).Include(a => a.product).Where(x => x.orderID == orderID).ToList();
                //UpdateOrderStatus(order.id);
                return View(_Details);
            }
        }

        private void UpdateOrderStatus(int oID)
        {
            
            using(OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                order order = db.orders.Where(x => x.id == oID).FirstOrDefault();
                order.actual_time = DateTime.Now;
                int uID = (Int32)(Session["id"]);
                order.staffID = uID;
                db.SaveChanges();
            }
        }
    }
}