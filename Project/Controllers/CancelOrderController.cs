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
                
                CreateCancelOrderTransaction(o.total_price, o.id);

                ViewBag.notice = "Order no: " + o.id + "cancle successed";
                return View();
            }
            
        }

        private void CreateCancelOrderTransaction(double amount, int orderID)
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                transaction trans = new transaction()
                {
                    userID = (Int32)(Session["id"]),
                    type = "CancelOrder",
                    amount = amount,
                    description = "Cancel order number: " + orderID ,
                    time = DateTime.Now
                };
                db.transactions.Add(trans);
                db.SaveChanges();

            }
        }
    }
}