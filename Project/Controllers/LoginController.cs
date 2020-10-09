using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
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
        public JsonResult GetInforFromGoogleAccount(string googleUser)
        {
            string google = googleUser;
            JObject googleObject = JObject.Parse(google);
            string googleInforName = GetJArrayValue(googleObject, "nt");

            JObject accountInfor = JObject.Parse(googleInforName);

            var googleId = GetJArrayValue(googleObject, "Ca");
            var userName = GetJArrayValue(accountInfor, "Ad");
            var image = GetJArrayValue(accountInfor, "ZJ");
            var email = GetJArrayValue(accountInfor, "Wt");

            
            

            OrderSystemEntities2 db = new OrderSystemEntities2();

            string encoded = EncodePassword(googleId);

            var userFounded = db.users.Where(x => x.email == email && x.password == encoded).FirstOrDefault();
            if (userFounded != null)
            {
                Session["id"] = userFounded.id;
                Session["role"] = 1;
                Session["user"] = userFounded;

                string url = "http://localhost:3000/get-information/" + userFounded.id;
                WebRequest myReq = WebRequest.Create(url);
                myReq.Method = "GET";
                myReq.ContentType = "application/json; charset=UTF-8";
                myReq.Headers.Add("key", "9849F97A8C5546C9906A059D1DD3EC64");



                WebResponse wr = myReq.GetResponse();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string content = reader.ReadToEnd();
                JObject jContent = JObject.Parse(content);

                var money = Int32.Parse(GetJArrayValue(jContent, "money"));
                userFounded.balance = money;
                Session["username"] = userFounded;

                db.Entry(userFounded).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                var u = new user();
                u.name = userName;
                u.username = userName;
                u.password = EncodePassword(googleId);
                u.address = "";
                u.phone_num = "";
                u.email = email;
                u.role = 1;
                u.avt_img = image;
                u.is_active = true;

                db.users.Add(u);
                db.SaveChanges();

                Session["id"] = u.id;
                Session["username"] = userName;
                Session["role"] = 1;
                Session["user"] = u;

                string url = "http://localhost:3000/get-information/" + u.id;
                WebRequest myReq = WebRequest.Create(url);
                myReq.Method = "GET";
                myReq.ContentType = "application/json; charset=UTF-8";
                myReq.Headers.Add("key", "9849F97A8C5546C9906A059D1DD3EC64");

                WebResponse wr = myReq.GetResponse();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string content = reader.ReadToEnd();

                JObject jContent = JObject.Parse(content);
                var money = Int32.Parse(GetJArrayValue(jContent, "money"));

                var user = db.users.Find(u.id);
                user.balance = money;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }

            Session["fromGoogle"] = 1;

            return Json("Success");
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

        private string GetJArrayValue(JObject yourJArray, string key)
        {
            foreach (KeyValuePair<string, JToken> keyValuePair in yourJArray)
            {
                if (key == keyValuePair.Key)
                {
                    return keyValuePair.Value.ToString();
                }
            }

            return null;
        }

    }
}