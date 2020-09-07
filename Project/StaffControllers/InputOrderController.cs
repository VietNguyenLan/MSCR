using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.EF;
using System.Web.Mvc;

namespace Project.StaffControllers
{
    public class InputOrderController : Controller
    {
        // GET: InputOrder
        public ActionResult Index(int orderID = -1, int code = -1)
        {
            if(orderID == -1 && code == -1)
            {
                return View();
            }
            else
            {
                using(OrderSystemEntities2 db  = new OrderSystemEntities2())
                {
                    order order = db.orders.Where(x => x.id == orderID && x.receive_code == code).FirstOrDefault();
                    if(order == null)
                    {
                        ViewBag.error = "Order number or receive code incorect!";
                        return View();
                    }
                    else
                    {
                        int service_time = -1;
                        var time_now = DateTime.Now;
                        TimeSpan start = new TimeSpan(06, 0, 0); //10 o'clock
                        TimeSpan end_breakfast = new TimeSpan(09, 0, 0); //12 o'clock
                        TimeSpan end_lunch = new TimeSpan(14, 0, 0); //12 o'clock
                        TimeSpan end_dinner = new TimeSpan(19, 0, 0); //12 o'clock
                        TimeSpan now = time_now.TimeOfDay;
                        
                        if(now > start && now < end_breakfast)
                        {
                            service_time = 1;
                        }else if(now > end_breakfast && now < end_lunch)
                        {
                            service_time = 2;
                        }else if(now > end_lunch && now < end_dinner)
                        {
                            service_time = 3;
                        }
                        else
                        {
                            ViewBag.error = "Not in service time!";
                            return View();
                        }





                        var today = time_now.Date;
                        if(order.take_date != today || order.take_time != service_time)
                        {
                            ViewBag.error = "Order was not for current time!";
                            return View();
                        }

                        if(order.take_date == today && order.take_time == service_time)
                        {
                            if(order.actual_time != null)
                            {
                                ViewBag.error = "Order aready serviced!";
                                return View();
                            }
                            else
                            {
                                return RedirectToAction("Index", "CurrentOrderDetail", orderID);
                            }

                        }

                    }
                }
                return View();
            }
        }
            
    }
}