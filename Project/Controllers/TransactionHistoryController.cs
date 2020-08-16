using Project.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class TransactionHistoryController : Controller
    {
        // GET: TransactionHistory
        public ActionResult Index()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                using(OrderSystemEntities1 db = new OrderSystemEntities1())
                {
                    int uID = (int)Session["id"];
                    List<transaction> transactions = db.transactions.OrderByDescending(x => x.time).Where(x => x.userID == uID).ToList();
                    return View(transactions);
                }
                
            }
        }
    }
}