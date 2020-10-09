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
        public ActionResult Index(order orders )
        {
            
                using(OrderSystemEntities2 db  = new OrderSystemEntities2())
                {
                    order order = db.orders.Where(x => x.id == orders.id && x.receive_code == orders.receive_code).FirstOrDefault();
                    if(order == null)
                    {
                        //ViewBag.error = "Order number or receive code incorect!";
                    return View();
                    }
                    else
                    {
                        int service_time = -1;
                        var time_now = DateTime.Now;
                        TimeSpan start = new TimeSpan(06, 0, 0); //10 o'clock
                        TimeSpan end_breakfast = new TimeSpan(09, 0, 0); //12 o'clock
                        TimeSpan end_lunch = new TimeSpan(14, 0, 0); //12 o'clock
                        TimeSpan end_dinner = new TimeSpan(21, 0, 0); //12 o'clock
                        TimeSpan now = time_now.TimeOfDay;
                    service_time = 1;
                    //if(now > start && now < end_breakfast)
                    //{
                    //    service_time = 1;
                    //}else if(now > end_breakfast && now < end_lunch)
                    //{
                    //    service_time = 2;
                    //}else if(now > end_lunch && now < end_dinner)
                    //{
                    //    service_time = 3;
                    //}
                    //else
                    //{
                    //    ViewBag.error = "Not in service time!";
                    //    return View();
                    //}





                    var today = time_now.Date;
                        if(order.take_date != today || order.take_time != service_time)
                        {
                        //return RedirectToAction("Index", "CurrentOrderDetail", new { orderID = orders.id, recieved_code = order.receive_code });

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
                                return RedirectToAction("Index", "CurrentOrderDetail", new { orderID = orders.id , recieved_code = order.receive_code});
                            }

                        }

                    }
                
                return View();
            }
        }

        public ActionResult IndexQR(int orderID, int recieved_code)
        {

            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                order _order = db.orders.Where(x => x.id == orderID && x.receive_code == recieved_code).FirstOrDefault();
                if (_order == null)
                {
                    ViewBag.error = "Order number or receive code incorect!";
                    return RedirectToAction("Index", "InputOrder", new { order = _order });
                }
                else
                {
                    int service_time = -1;
                    var time_now = DateTime.Now;
                    TimeSpan start = new TimeSpan(06, 0, 0); //10 o'clock
                    TimeSpan end_breakfast = new TimeSpan(09, 0, 0); //12 o'clock
                    TimeSpan end_lunch = new TimeSpan(14, 0, 0); //12 o'clock
                    TimeSpan end_dinner = new TimeSpan(21, 0, 0); //12 o'clock
                    TimeSpan now = time_now.TimeOfDay;
                    service_time = 1;
                    //if(now > start && now < end_breakfast)
                    //{
                    //    service_time = 1;
                    //}else if(now > end_breakfast && now < end_lunch)
                    //{
                    //    service_time = 2;
                    //}else if(now > end_lunch && now < end_dinner)
                    //{
                    //    service_time = 3;
                    //}
                    //else
                    //{
                    //    ViewBag.error = "Not in service time!";
                    //    return View();
                    //}





                    var today = time_now.Date;
                    if (_order.take_date != today || _order.take_time != service_time)
                    {
                        return RedirectToAction("Index", "InputOrder", new { order = _order });
                        //return RedirectToAction("Index", "CurrentOrderDetail", new { orderID = _order.id, recieved_code = _order.receive_code });

                        //ViewBag.error = "Order was not for current time!";
                        //return RedirectToAction("Index", "InputOrder", new { order = _order });
                    }

                    if (_order.take_date == today && _order.take_time == service_time)
                    {
                        if (_order.actual_time != null)
                        {
                            //ViewBag.error = "Order aready serviced!";
                            return RedirectToAction("Index", "InputOrder", new { order = _order });
                        }
                        else
                        {
                            return RedirectToAction("Index", "CurrentOrderDetail", new { orderID = _order.id, recieved_code = _order.receive_code });
                           // return RedirectToAction("Index", "InputOrder", new { order = _order });
                        }

                    }

                }

                return RedirectToAction("Index", "InputOrder", new { order = _order });
            }
        }

    }
}