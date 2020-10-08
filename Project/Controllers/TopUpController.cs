using Project.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class TopUpController : Controller
    {
        // GET: TopUp

        public ActionResult TopUp(String code)
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {

          
                var card = (topup_card)db.topup_card.Where(x =>  x.code == code).FirstOrDefault();
                if(card != null)
                {
                    if (card.used_by != null)
                    {
                        ViewBag.IsUsed = 1;
                    }

                    else
                    {
                        card.used_by = (Int32)(Session["id"]);
                    card.used_time = DateTime.Now;
                    
                    db.SaveChanges();
                   
                    transaction trans = new transaction()
                    {
                        userID = (Int32)(Session["id"]),
                        type = "Top up",
                        amount = card.value,
                        description = "Top up " + card.value + " using card with serial: " + card.serial_number,
                        time = DateTime.Now
                    };
                    db.transactions.Add(trans);
                    db.SaveChanges();

                       
                        ViewBag.success = 1;
                        ViewBag.cardValue = card.value;
                        
                        int userId = (Int32)(Session["id"]);
                        user u = db.users.Where(x => x.id == userId).FirstOrDefault();
                        Session["user"] = u;

                        SendActivationEmail(u, card);
                    }

                      
                       return View();
                }
                else
                {
                    
                    return View();
                   
                }

                

                 
            }
            
        }

        private void SendActivationEmail(user user, topup_card card)
        {


            using (MailMessage mm = new MailMessage("nguyenanhyoung@gmail.com", user.email))
            {
                mm.Subject = "Nạp Tiền Thành Công";
                string body = "Xin chào " + user.username + ",";
                body += "<br /><br />Bạn đã nạp thành công "+ card.value +" nghìn đồng từ thẻ nạp có số series: " + card.serial_number;
             

                body += "<br /><br />Cảm ơn bạn đã sử dụng dịch vụ và chúc bạn một ngày tốt lành !";
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



        private void CreateTopUpTransaction(int amount, String serial)
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                transaction trans = new transaction()
                {
                    userID = (Int32)(Session["id"]),
                    type = "Top up",
                amount = amount,
                description = "Top up " + amount + " using card with serial: " + serial,
                time = DateTime.Now
                };
                db.transactions.Add(trans);
                db.SaveChanges();
                
            }
        }
    }
}