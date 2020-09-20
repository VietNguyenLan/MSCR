using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Project.EF;

namespace Project.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authourise(user user)
        {
            using (OrderSystemEntities2 od = new OrderSystemEntities2())
            {
                string encoded = EncodePassword(user.password);
               
                var userDetails = od.users.Where(x => x.username == user.username && x.password == encoded).FirstOrDefault();
                
                if(user.username == "" || user.password == "")
                {
                    return View("Index", user);
                }              
                else if(userDetails == null)
                {
                    user.LoginErrorMsg = "Invalid username or password";
                    return View("Index", user);
                }
                else
                {
                    if (userDetails.role.Equals(2))
                    {
                        Session["id"] = userDetails.id;
                        Session["username"] = userDetails.username;
                        Session["user"] = userDetails;
                        Session["role"] = userDetails.role;
                        return RedirectToAction("Index", "InputOrder");

                    }
                    else
                    {
                        Session["id"] = userDetails.id;
                        Session["username"] = userDetails.username;
                        Session["user"] = userDetails;
                        Session["role"] = userDetails.role;
                        return RedirectToAction("Home", "Home");
                    }
                }
            }
        }

        public string EncodePassword(string originalPassword)
        {
            //Declarations
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
            encodedBytes = md5.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes);
        }

        public ActionResult LogOut()
        {
            int userID = (int)Session["id"];
            Session.Abandon();
            return RedirectToAction("Home", "Home");
        }
    
    }
}