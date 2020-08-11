using Project.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class TopUpController : Controller
    {
        // GET: TopUp
        public ActionResult Index(int? code = 0)
        {

            int uid = (Int32)(Session["id"]);
            List<topup_card> cards = new List<topup_card>();
            using(OrderSystemEntities1 db = new OrderSystemEntities1())
            {
                cards = db.topup_card.OrderByDescending(x => x.create_time).Where(x => x.used_by == uid).ToList();
            }
            if(code != 0 && code != 1)
            {
                TempData["TopUp Success"] = "Top up success "+code+" to your account";
            }
            if (code == 1)
            {
                TempData["TopUp Error"] = "Serial number or Code of the card incorrect";
            }


            return View(cards);
        }

        public ActionResult TopUp(String serial, String code)
        {
            using (OrderSystemEntities1 db = new OrderSystemEntities1())
            {
                var card = (topup_card)db.topup_card.Where(x => x.serial_number == serial && x.code == code).FirstOrDefault();
                if(card!= null)
                {
                    card.used_by = (Int32)(Session["id"]);
                    card.used_time = DateTime.Now;
                    db.SaveChanges();
                    return Index(card.amount);
                }
                else
                {

                }

                return Index();
            }
            
        }
    }
}