using Project.EF;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class CheckOutController : Controller
    {
        // GET: CheckOut
        public ActionResult CheckOut()
        {
            List<CartItem> list = (List<CartItem>)Session["cart"];
            if (list.Count == 0)
            {
                return View();
            }
            else
            {
                ViewBag.totalPrice = (int)list.Sum(x => x.totalProduct);

                return View(list);
            }
        }
        public ActionResult CreateOrder()
        {
            int uID = (Int32)(Session["id"]);
            using (OrderSystemEntities1 db = new OrderSystemEntities1())
            {
                user u = db.users.Where(x => x.id == uID).FirstOrDefault();
                List<CartItem> items = (List<CartItem>)Session["cart"];
                DateTime date = items[0].Date;
                int service_time = items[0].ServiceTime;
                float total_price = (float)items.Sum(x => x.Product.price * x.Quantity);
                if (total_price > u.balance)
                {
                    ViewBag.BalanceError = 1;
                }
                else
                {
                    order order = new order()
                    {
                        userID = (Int32)(Session["id"]),
                        create_time = DateTime.Now,
                        take_date = date,
                        take_time = service_time,
                        is_cancle = false,
                        total_price = total_price,
                        reviewed = false,
                        receive_code = RandReceiveCode()
                    };
                    db.orders.Add(order);
                    db.SaveChanges();
                    int orderID = order.id;

                    foreach (CartItem item in items)
                    {
                        order_detail order_Detail = new order_detail()
                        {
                            orderID = orderID,
                            productID = item.Product.id,
                            quantity = item.Quantity,
                            price = item.Product.price,
                            total_price = item.Quantity * item.Product.price
                        };
                        db.order_detail.Add(order_Detail);
                    }
                    db.SaveChanges();
                    AddOrderToTransaction(order);
                    ViewBag.BalanceError = 0;

                }


            }
            return RedirectToAction("Home", "Home");
        }



        private void AddOrderToTransaction(order order)
        {
            using (OrderSystemEntities1 db = new OrderSystemEntities1())
            {
                transaction trans = new transaction()
                {
                    userID = (Int32)(Session["id"]),
                    type = "Order Pay",
                    amount = order.total_price * -1,
                    description = "Pay " + order.total_price + " for order number:  " + order.id,
                    time = DateTime.Now
                };
                db.transactions.Add(trans);
                db.SaveChanges();
            }
        }

        private int RandReceiveCode()
        {
            Random rnd = new Random();
            return rnd.Next(1000, 10000);
        }
    }
}