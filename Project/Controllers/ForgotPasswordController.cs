using Project.EF;
using Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class ForgotPasswordController : Controller
    {
        // GET: ForgotPassword
        OrderSystemEntities2 db = new OrderSystemEntities2();
        public ActionResult Index()
        {
            return View();
        }

        private void SendActivationEmail(user user)
        {
          
            var useremail = db.users.Where(p => p.email == user.email).FirstOrDefault();

            using (MailMessage mm = new MailMessage("nguyenanhyoung@gmail.com", useremail.email))
            {
                mm.Subject = "Rest Password";
                string body = "Hello " + user.username + ",";
                body += "<br /><br />Here is your passowrd:" + useremail.password + "";
                body += "<br /><br />Please click the following link to login your account";
                body += "<br /><a href = '" + this.Url.Action("Index", "Login", null, this.Request.Url.Scheme) + "'>Click here !</a>";
                body += "<br /><br />Thanks";
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

        [HttpPost]
        public ActionResult ForgotPassword(ForgotEmailViewModels forgotEmailViewModels)
        {
            var forgotpassword = db.users.Where(p => p.email == forgotEmailViewModels.Email).FirstOrDefault();
            if (forgotpassword != null)
            {
                SendActivationEmail(forgotpassword);
                forgotEmailViewModels.ErrorMgs = "Please check your email !";
            }
            else
            {
                forgotEmailViewModels.ErrorMgs = "Wrong email, please check again !";
            }
           
            return View("Index",forgotEmailViewModels);
        }

        

    }
}