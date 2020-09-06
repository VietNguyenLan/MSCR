using Project.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Project.Controllers
{
    public class FeedbackController : Controller
    {
        // GET: Feedback
        public ActionResult Feedback(int oID)
        {
            using(OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                var feedback = db.orders.Where(x => x.id == oID).FirstOrDefault();
              
               

                return View(feedback);
            }
            
        }
     
        public ActionResult Add_Feedback(int oID, String content)
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                int uid = (Int32)(Session["id"]);
                var user_check = db.feed_back.Where(a => a.userID == uid).FirstOrDefault();
                if (user_check != null)
                {
                    ViewBag.IsFeedback = "Bạn đã FeedBack";
                    return RedirectToAction("Index", "OrderList");
                }
                else
                {
                var fb = new feed_back();
                fb.orderID = oID;
                fb.create_time = DateTime.Now;
                fb.content = content;
                fb.userID = (Int32)(Session["id"]);
                db.feed_back.Add(fb);
                db.SaveChanges();
                }
                

                

            }
            return RedirectToAction("Index", "OrderList");
        }
    }




}