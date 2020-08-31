using Project.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Project.Security;

namespace Project.AdminController
{
    public class ReportIncomeController : Controller
    {
        // GET: ReportIncome
        [DeatAuthorize(Order = 3)]
        public ActionResult Index()
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                DateTime date = DateTime.Now.Date;

                ViewBag.total = db.orders.Where(x => x.take_date == date).Select(i => i.total_price).Sum();
                return View(db.orders.Include(a => a.user).Include(b => b.service_time).Where(x => x.take_date == date).ToList());
            }
        }
    }
}