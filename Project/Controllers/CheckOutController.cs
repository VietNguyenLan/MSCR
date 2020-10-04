using Project.EF;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class CheckOutController : Controller
    {
        OrderSystemEntities2 db = new OrderSystemEntities2();
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
                int uID = (Int32)(Session["id"]);
                int total = (int)list.Sum(x => x.totalProduct);
                ViewBag.totalPrice = total;
                using(OrderSystemEntities2 db = new OrderSystemEntities2())
                {
                    user u = db.users.Where(x => x.id == uID).FirstOrDefault();
                    if(total > u.balance)
                    {
                        ViewBag.BalanceError = 1;
                    }
                }

                return View(list);
            }
        }
        public ActionResult CreateOrder()
        {
            int uID = (Int32)(Session["id"]);
            int oID = 0;
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
               
            {
                user u = db.users.Where(x => x.id == uID).FirstOrDefault();
                List<CartItem> items = (List<CartItem>)Session["cart"];
                DateTime date = items[0].Date;
                int service_time = items[0].ServiceTime;
                float total_price = (float)items.Sum(x => x.Product.price * x.Quantity);
                if (total_price > u.balance)
                {
                    ViewBag.BalanceError = 1;
                    return RedirectToAction("CheckOut", "CreateOrder");
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
                    oID = orderID;
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

                    SendActivationEmail(u , orderID);

                    Session.Remove("cart");
                }


            }
            return RedirectToAction("Index", "OrderDetail",new { oID = oID });
        }

       
        private void SendActivationEmail(user user , int orderID)
        {
            

            using (MailMessage mm = new MailMessage("nguyenanhyoung@gmail.com", user.email))
            {
                mm.Subject = "Đặt Hàng Thành Công";
                string body = "Xin chào " + user.username + ",";
                body += "<br /><br />Bạn đã đặt hàng thành công !";
                body += "<br /><br />Hãy ấn vào đường link dưới đây để xem chi tiết đơn hàng của bạn";
                body += "<br /><br /> http://localhost:51293/OrderDetail?oID="+orderID;
                body += "<br /><br />Cảm ơn và chúc bạn một ngày tốt lành !";
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("nguyenanhyoung@gmail.com", "anhdaica1");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
        }



        private void AddOrderToTransaction(order order)
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
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

                int userId = (Int32)(Session["id"]);
                user us = db.users.Where(x => x.id == userId).FirstOrDefault();
                Session["user"] = us;
            }
        }

        private int RandReceiveCode()
        {
            Random rnd = new Random();
            return rnd.Next(1000, 10000);
        }
    }
}