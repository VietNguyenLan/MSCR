using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.EF;

namespace Project.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        OrderSystemEntities1 db = new OrderSystemEntities1();
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
          

            if (result.Count() != 0)
            {
                 user.LoginErrorMsg = "Username Is Already In Use";

            }

            
             
           else
            {
                try
                {
                user u = new user();
                string path = UpLoadImage(picture);

                    u.name = user.name;
                    u.username = user.username;
                    u.password = user.password;
                    u.address = user.address;
                    u.phone_num = user.phone_num;
                    u.email = user.email;
                    u.role = user.role;
                    u.avt_img = path;
                    u.is_active = user.is_active;
                
                    db.users.Add(u);
                db.SaveChanges();
                user.LoginErrorMsg = "data added";
                return RedirectToAction("Index", "Login");
                }
                catch(Exception ex)
                {
                    
                }
                

            }
            return View("Index", user);
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