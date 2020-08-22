using Project.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Project.Controllers
{
    public class TransactionHistoryController : Controller
    {
        // GET: TransactionHistory
        public ActionResult History()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                using(OrderSystemEntities2 db = new OrderSystemEntities2())
                {
                    int uID = (int)Session["id"];
                    List<transaction> transactions = db.transactions.Include(a => a.user).OrderByDescending(x => x.time).Where(x => x.userID == uID).ToList();
                    return View(transactions);
                }
                
            }
        }
    }
}