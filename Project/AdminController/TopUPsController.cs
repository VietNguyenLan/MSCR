﻿using Project.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using Project.Security;

namespace Project.AdminController
{
    public class TopUPsController : Controller
    {
        // GET: TopUP
        [DeatAuthorize(Order = 3)]
        public ActionResult Index()
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                return View(db.topup_card.Include(c => c.user).ToList());
            }
        }

        // GET: TopUP/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TopUP/Create
        public ActionResult Create()
        {
            return View();
        }

        public string Get8CharacterRandomString()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            return path.Substring(0, 8).ToUpper();  // Return 8 character string
        }
        // POST: TopUP/Create
        [HttpPost]
        public ActionResult Create(topup_card topup_Card)
        {
            try
            {
                for (int i = 0; i < topup_Card.amount; i++)
                {


                    using (OrderSystemEntities2 db = new OrderSystemEntities2())
                    {
                        int uID = (Int32)(Session["id"]);
                        string seri = Get8CharacterRandomString();
                        string code = Get8CharacterRandomString();
                        topup_Card.serial_number = seri;
                        topup_Card.code = code;
                        topup_Card.create_time = DateTime.Now;
                        topup_Card.creator = uID;
                        db.topup_card.Add(topup_Card);
                        db.SaveChanges();
                    }

                    
                }

               return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TopUP/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TopUP/Edit/5
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

      

       
    }
}
