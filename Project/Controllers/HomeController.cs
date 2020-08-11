using Project.EF;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(DateTime? date, int? service_time)
        {
            if (date is null)
            {
                date = DateTime.Now.Date;
            }

            if (service_time is null)
            {
                service_time = 1;
            }

            using (OrderSystemEntities1 db = new OrderSystemEntities1())

            {
                var date_menu = db.time_menu.SqlQuery("select * from menu_detail where date_service ='" + date + "'").FirstOrDefault();
                int menuID = (int)date_menu.breakfast_mId;
                if (service_time == 2)
                {
                    menuID = (int)date_menu.lunch_mId;
                }
                if (service_time == 3)
                {
                    menuID = (int)date_menu.dinner_mId;
                }

                List<product> productList = new List<product>();

                var menu_details = db.menu_detail.Where(x => x.menuId == menuID).ToList();
                foreach (menu_detail item in menu_details)
                {
                    product p = db.products.Where(x => x.id == item.productID).FirstOrDefault();
                    productList.Add(p);
                }

                List<CartItem> items = new List<CartItem>();



                return View(productList);
            }

        }
    }
}