using Project.EF;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class OrderDetailController : Controller
    {
        // GET: OrderDetail
        public ActionResult Index(order o)
        {
            using (OrderSystemEntities1 db = new OrderSystemEntities1())
            {
                List<order_detail> _Details = new List<order_detail>();
                _Details = db.order_detail.Where(x => x.orderID == o.id).ToList();
                List<product> _products = new List<product>();
                foreach (order_detail item in _Details)
                {

                    _products.Add((product)db.products.Where(x => x.id == item.productID));
                }


                dynamic model = new ExpandoObject();
                model.order = o;
                model.order_detail = _Details;
                model.products = _products;
                return View(model);
            }
        }
    }
}