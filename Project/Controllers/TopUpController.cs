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

        public ActionResult TopUp(String code)
        {
            using (OrderSystemEntities1 db = new OrderSystemEntities1())
            {
                var card = (topup_card)db.topup_card.Where(x =>  x.code == code).FirstOrDefault();
                if(card!= null)
                {
                    card.used_by = (Int32)(Session["id"]);
                    card.used_time = DateTime.Now;
                    
                    db.SaveChanges();
                   
                    transaction trans = new transaction()
                    {
                        userID = (Int32)(Session["id"]),
                        type = "Top up",
                        amount = card.value,
                        description = "Top up " + card.value + " using card with serial: " + card.serial_number,
                        time = DateTime.Now
                    };
                    db.transactions.Add(trans);
                    db.SaveChanges();
                    return View();
                }
                else
                {
                    return View();
                }

                
            }
            
        }



        private void CreateTopUpTransaction(int amount, String serial)
        {
            using (OrderSystemEntities1 db = new OrderSystemEntities1())
            {
                transaction trans = new transaction()
                {
                    userID = (Int32)(Session["id"]),
                    type = "Top up",
                amount = amount,
                description = "Top up " + amount + " using card with serial: " + serial,
                time = DateTime.Now
                };
                db.transactions.Add(trans);
                db.SaveChanges();
                
            }
        }
    }
}