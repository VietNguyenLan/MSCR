using Project.EF;
using Project.Models;
using Project.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.AdminController
{
    public class ProductReportController : Controller
    {
        // GET: ProductReport
        [DeatAuthorize(Order = 3)]
        public ActionResult Index()
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                DateTime date = DateTime.Now.Date;
                date = date.AddDays(1).Date;
                List<order> orders = db.orders.Where(x => x.take_date == date).ToList();
                List<order_detail> order_Details = new List<order_detail>();
                foreach (var order in orders)
                {
                    order_Details.AddRange(db.order_detail.Where(x => x.orderID == order.id).ToList());
                }
                List<product> products = new List<product>();
                foreach (var order_detail in order_Details)
                {
                    products.Add(order_detail.product);
                }

                List<ProductPrepare> productPrepares = new List<ProductPrepare>();
                foreach (product pro in products)
                {
                    int index = IsExist(pro, productPrepares);
                    if (index == -1)
                    {
                        productPrepares.Add(new ProductPrepare()
                        {
                            Product = pro,
                            Quantity = 1
                        });
                    }
                    else
                    {
                        productPrepares[index].Quantity += 1;

                    }
                }

                return View(productPrepares);
            }
        }
        private int IsExist(product p, List<ProductPrepare> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Product.id.Equals(p.id))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}