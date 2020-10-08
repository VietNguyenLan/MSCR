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

                    SendActivationEmail(u , order);

                    Session.Remove("cart");
                }


            }
            return RedirectToAction("Index", "OrderDetail",new { oID = oID });
        }

       
        private void SendActivationEmail(user user , order order)
        {
            

            using (MailMessage mm = new MailMessage("nmtien2502@gmail.com", user.email))
            {
                mm.Subject = "Đặt Hàng Thành Công";

              
                string body = "<div style='font - family: Segoe UI; margin: 0; color: #707070;font-size:16px;'>";
                body += "<div style='max - width:800px; width: 100 %; margin: 0 auto; '>";
                body += "<img style='width: 100 % ' src='https://i.pinimg.com/564x/f9/98/86/f99886a97ba4e7b0aa4d8b33e00b060c.jpg' />";
                body += "<div style='padding: 1.5rem; color: #707070;'>";
                body += " <h3 style='color:#069B4F; font-size:24px;'>Đặt hàng thành công</h3>";
                body += "Xin chào " + user.name + ",";
                body += "<br /><br />Bạn đã đặt hàng thành công order số: "+ order.id;
                body += "<br /><br />Mã nhận đơn của bạn là: "+ order.receive_code;
                body += "<br /><br />Hãy nhớ ngày đến lấy đồ ăn của bạn: " + order.take_date;
                body += "<br /><br />Tổng hóa đơn của bạn là: " + order.total_price + "nghìn đồng!";
                body += "<br /><br />Hãy ấn vào đường link dưới đây để xem chi tiết đơn hàng của bạn";
                body += "<br /><br /> http://localhost:51293/OrderDetail?oID="+order.id;
                body += "<br /><br />Cảm ơn và chúc bạn một ngày tốt lành !";
                body += "</div>";
                body += "</div>";
                body += "</div>";
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