using Project.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Project.Controllers
{
    public class FeedbackController : Controller
    {
        // GET: Feedback
        public ActionResult Feedback(order o)
        {
            using(OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                var feedback = (feed_back)db.feed_back.Where(x => x.orderID == o.id).FirstOrDefault();
                if(feedback != null)
                {
                    return View(feedback);
                }
                if(o.staffID == null)
                {
                    ViewBag.Message = "You can not write feedback for un-service order";
                }

                return View();
            }
            
        }

        public ActionResult Add_Feedback(order o, String content)
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                var fb = new feed_back();
                fb.orderID = o.id;
                fb.create_time = DateTime.Now;
                fb.content = content;
                fb.userID = (Int32)(Session["id"]);
                db.feed_back.Add(fb);
                db.SaveChanges();

            }
            return Feedback(o);
        }
    }




}