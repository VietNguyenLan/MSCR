﻿using Project.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.Entity;
using Project.Security;
using PagedList;

namespace Project.AdminController
{
    public class MenuDailyController : Controller
    {
        OrderSystemEntities2 db = new OrderSystemEntities2();
        // GET: MenuDaily
        [DeatAuthorize(Order = 3)]
        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            int pageSize = 5;


            int pageNumber = (page ?? 1);
            var time = db.time_menu.Include(c => c.menu).Include(a => a.menu1).Include(b => b.menu2).ToList().OrderByDescending(c => c.date_service);
            return View(time.ToPagedList(pageNumber, pageSize));

        }
        

        // GET: MenuDaily/Details/5
        public ActionResult Details(DateTime date_service)
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                return View(db.time_menu.SqlQuery("select * from time_menu where date_service ='"  +date_service.Date + "'").FirstOrDefault());
            }
        }

        // GET: MenuDaily/Create
        public ActionResult Create()
        {
            SetViewBag();
            SetViewBag2();
            SetViewBag3();
            return View();
        }

        // POST: MenuDaily/Create
        [HttpPost]
        public ActionResult Create(time_menu time_Menu)
        {
            try
            {
                using (OrderSystemEntities2 db = new OrderSystemEntities2())
                {
                    time_menu tM = new time_menu();

                    tM.date_service = time_Menu.date_service;
                    tM.breakfast_mId = time_Menu.breakfast_mId;
                    tM.lunch_mId = time_Menu.lunch_mId;
                    tM.dinner_mId = time_Menu.dinner_mId;
                    
                    db.time_menu.Add(tM);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public void SetViewBag(long? breakfast_mId = null)
        {
            ViewBag.breakfast_mId = new SelectList(ListAll(), "id", "menu_name", breakfast_mId);
        }
        public void SetViewBag2(long? lunch_mId = null)
        {
            ViewBag.lunch_mId = new SelectList(ListAll(), "id", "menu_name", lunch_mId);
        }
        public void SetViewBag3(long? dinner_mId = null)
        {
            ViewBag.dinner_mId = new SelectList(ListAll(), "id", "menu_name", dinner_mId);
        }
        public List<menu> ListAll()
        {
            return db.menus.Where(x => x.disable == false).ToList();
        }

        // GET: MenuDaily/Edit/5
        public ActionResult Edit()
        {
            return View();
        }

        // POST: MenuDaily/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MenuDaily/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MenuDaily/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
