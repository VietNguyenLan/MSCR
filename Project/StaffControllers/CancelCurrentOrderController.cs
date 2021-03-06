﻿using Project.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.StaffControllers
{
    public class CancelCurrentOrderController : Controller
    {
        // GET: CancelCurrentOrder

        public ActionResult Index(int oID)
        {
            ViewBag.oID = oID;
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                order o = db.orders.Where(x => x.id == oID).FirstOrDefault();

                o.is_cancle = true;
                db.SaveChanges();

                transaction trans = new transaction()
                {
                    userID = o.userID,
                    type = "Cancel",
                    amount = o.total_price,
                    description = "Cancel order:  " + oID,
                    time = DateTime.Now
                };
                db.transactions.Add(trans);
                db.SaveChanges();

            }
            return RedirectToAction("Index", "QRScanner");
        }

    public ActionResult Cancel(int oID, int otp, string reason)
        {
            using(OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                otp_table _otp = db.otp_table.Where(x => x.otp == otp).FirstOrDefault();
                if(_otp == null)
                {
                    ViewBag.error = "Mã xác thực sai, vui lòng kiểm tra lại!";
                    return View();
                }
                else
                {
                    var otp_time = _otp.create_time;
                    var now = DateTime.Now;
                    if((now - otp_time).TotalMinutes > 2)
                    {
                        ViewBag.error = "Mã xác thực đã hết hạn, vui lòng kiểm tra lại";
                        return View();
                    }
                    else
                    {
                        UpdateCancel(oID, reason);
                        return RedirectToAction("Index", "QRScanner");
                    }

                }

            }  
        }

        private void UpdateCancel(int oID,string reason)
        {
            using(OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                order o = db.orders.Where(x => x.id == oID).FirstOrDefault();

                o.is_cancle = true;
                db.SaveChanges();

                transaction trans = new transaction()
                {
                    userID = (Int32)(Session["id"]),
                    type = "Cancel",
                    amount = o.total_price,
                    description = "Cancel order:  " + oID + ":  " + reason,
                    time = DateTime.Now
                };
                db.transactions.Add(trans);
                db.SaveChanges();

            }
        }
    }
}