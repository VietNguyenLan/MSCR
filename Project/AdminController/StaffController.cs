using Project.EF;
using Project.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Project.AdminController
{
    public class StaffController : Controller
    {
        // GET: Staff
        OrderSystemEntities2 db = new OrderSystemEntities2();
        [DeatAuthorize(Order = 3)]
        public ActionResult Index()
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                return View(db.users.Where(x => x.role == 2).Where(x => x.is_active == true).ToList());
            }
        }



        // GET: Staff/Create
        public ActionResult Create()
        {
            return View();
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

        // POST: Staff/Create
        [HttpPost]
        public ActionResult Create(user user, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                OrderSystemEntities2 db = new OrderSystemEntities2();
                user u = new user();

                u.name = user.name;
                u.username = user.username;
                u.password = EncodePassword(user.password);
                u.address = user.address;
                u.phone_num = user.phone_num;
                u.email = user.email;
                u.role = 2;
                u.avt_img = "123";
                u.is_active = true;
                u.email_verified = true;

                db.users.Add(u);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Staff/Edit/5
        public ActionResult Edit(int id)
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {

                return View(db.users.Where(x => x.id == id).FirstOrDefault());
            }
        }

        // POST: Staff/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, user user)
        {
            try
            {
                using (OrderSystemEntities2 db = new OrderSystemEntities2())
                {
                   
                    user.avt_img = "abc";
                    user.email = "abc";
                    user.address = "abc";
                    user.phone_num = "1";
                    user.username = "abc";
                    user.name = "abc";
                    user.password = "abc";
                    user.is_active = false;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }


        }
     }
}

