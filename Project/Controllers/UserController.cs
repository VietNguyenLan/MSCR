using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.EF;
using Project.ViewModels;


namespace Project.Controllers
{
    public class UserController : Controller
    {
        OrderSystemEntities2 od = new OrderSystemEntities2();   

        public ActionResult UserDetails(int id)
        {
            int uid = (Int32)Session["id"];
            var user = od.users.Find(uid);
            return View("Edit",user);
        }


        //public bool Update(user entity, HttpPostedFileBase picture)
        //{
        //    try
        //    {
        //        string path = UpLoadImage(picture);
        //        int uid = (Int32)Session["id"];
        //        var user = od.users.Find(uid);
        //        user.name = entity.name;
        //        user.address = entity.address;
        //        user.phone_num = entity.phone_num;
        //        user.role = entity.role;
        //        user.email = entity.email;
        //        user.avt_img = path;
        //        od.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
        // up img
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
                    catch (Exception ex)
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

        [HttpPost]
        public ActionResult Edit(user user1, HttpPostedFileBase picture)
        {
            string path = UpLoadImage(picture);

            if (!ModelState.IsValid)
            {
                try
                {
                    int uid = (Int32)Session["id"];
                    var user = od.users.Find(uid);
                    user.name = user1.name;
                    user.address = user1.address;
                    user.phone_num = user1.phone_num;
                    user.role = user1.role;
                    user.email = user1.email;
                    user.avt_img = path;
                    user.is_active = user1.is_active;
                    od.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Update has fail !");
                }
                
            }
            return View();
           
        }

        public ActionResult Index()
        {
            return View();
        }


       
    }
}