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
                ViewBag.total = db.order_detail.Where(t => t.orderID == oID).Select(i => i.total_price).Sum();
                List<order_detail> _Details = new List<order_detail>();
                _Details = db.order_detail.Include(o => o.order).Include(a => a.product).Where(x => x.orderID == oID ).ToList();
                return View(_Details);
            }
        }
    }
}