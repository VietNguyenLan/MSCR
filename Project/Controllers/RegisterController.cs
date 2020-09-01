using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Project.EF;
using Project.ViewModels;

namespace Project.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        OrderSystemEntities2 db = new OrderSystemEntities2();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(user user , HttpPostedFileBase picture)
        {
            var result = (from row in db.users
                          where row.username == user.username
                          select row).ToList();

            var custemail = (from row in db.users
                             where row.email == user.email
                             select row).ToList();

            user.email_verified = false;

            if (result.Count() != 0)
            {
                 user.LoginErrorMsg = "Username Is Already In Use";

            }
            if (custemail.Count() != 0)
            {
                user.LoginErrorMsg = "Email is already in use";
            }

            else
           {
               
                var u = new user();
                string path = UpLoadImage(picture);

                    u.name = user.name;
                    u.username = user.username;
                    u.password = user.password;
                    u.address = user.address;
                    u.phone_num = user.phone_num;
                    u.email = user.email;
                    u.role = 1;
                    u.avt_img = "~/Style/avatar/985985854default-avatar.png";
                    u.is_active = user.is_active;
                
                    db.users.Add(u);
                    db.SaveChanges();
                    SendActivationEmail(u);
                    user.LoginErrorMsg = "Please check your email !";
               
                
            }
            return View("Index", user);
        }

        private void SendActivationEmail(user user)
        {
            Random rand = new Random();
            int otpcode = rand.Next(minValue: 1000, maxValue: 10000);
            otp_table otp = db.otp_table.Add(new otp_table
            {
                uId = user.id,
                otp = otpcode,
                create_time = DateTime.Now
            });
            db.SaveChanges();

            using (MailMessage mm = new MailMessage("nguyenanhyoung@gmail.com", user.email))
            {
                mm.Subject = "Account Activation";
                string body = "Hello " + user.username + ",";
                body += "<br /><br />Thanks for the confirmation and enjoy your day!";
                body += "<br /><br />Please click the following link to login your account";
                body += "<br /><a href = '" + string.Format("{0}://{1}/Register/Activation/{2}", Request.Url.Scheme, Request.Url.Authority, otpcode) + "'>Click here !</a>";
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

        public ActionResult Activation()
        {
            if (RouteData.Values["id"] != null)
            {
                int activationCode = Convert.ToInt32(RouteData.Values["id"]);
                var userActivation = db.users.Where(p => p.otp_table.otp == activationCode).FirstOrDefault();
                if (userActivation != null)
                {
                    return RedirectToAction("Index", "Login");
                }
            }

            return View();
        }



        public string UpLoadImage(HttpPostedFileBase picture)
        {
            Random r = new Random();
            string path = "-1";
            int random = r.Next();

            if (picture != null && picture.ContentLength > 0)
            {
                string extension = Path.GetExtension(picture.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("~/Style/avatar"), random + Path.GetFileName(picture.FileName));
                        picture.SaveAs(path);
                        path = "~/Style/avatar/" + random + Path.GetFileName(picture.FileName);
                    }
                    catch(Exception ex)
                    {
                        path = "-2";
                    }

                }
                else
                {
                    Response.Write("<script>arlert('Only jpg , jpeg or png formats are acceptable.....');</script>");
                }
            }
            else
            {
                Response.Write("<script>arlert('Please select a file');</script>");
                path = "-3";
            }

            return path;
        }
    }


}