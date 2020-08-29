using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.EF;
using System.Web.Mvc;
using System.Dynamic;

namespace Project.StaffControllers
{
    public class CurrentOrderDetailController : Controller
    {
        // GET: CurrentOrderDetail
        public ActionResult Index(order o)
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                List<order_detail> _Details = new List<order_detail>();
                _Details = db.order_detail.Where(x => x.orderID == o.id).ToList();
                List<product> _products = new List<product>();
                foreach (order_detail item in _Details)
                {

                    _products.Add((product)db.products.Where(x => x.id == item.productID));
                }
                UpdateOrderStatus(o);

                dynamic model = new ExpandoObject();
                model.order = o;
                model.order_detail = _Details;
                model.products = _products;
                return View(model);
            }
        }

        private void UpdateOrderStatus(order o)
        {
            using(OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                o.actual_time = DateTime.Now;
                db.SaveChanges();
            }
        }
    }
}