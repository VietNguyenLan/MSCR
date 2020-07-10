﻿using Project.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.AdminController
{
    public class ProductController : Controller
    {
        OrderSystemEntities1 db = new OrderSystemEntities1();
        // GET: Product
        public ActionResult Index()
        {
            using (OrderSystemEntities1 db = new OrderSystemEntities1())
            {
                return View(db.products.ToList());
            }
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            using(OrderSystemEntities1 db = new OrderSystemEntities1())
            {
                return View(db.products.Where(x => x.id == id).FirstOrDefault());
            }
           
        }
       

        // GET: Product/Create
        public ActionResult Create()
        {
            try
            {
                using(OrderSystemEntities1 db = new OrderSystemEntities1())
                {

                    SetViewBag();
                }
            }
            catch
            {

            }
            return View();
        }


        //get list category
        public void SetViewBag(long? categoryID = null )
        {
            ViewBag.categoryID = new SelectList(ListAll(), "id", "name" , categoryID);
        }
        public List<category> ListAll()
        {
            return db.categories.Where(x => x.disable == true).ToList();
        }
        //get list category


        //Upload Image////////////////////////
        public string UpLoadImage(HttpPostedFileBase picture)
        {
            Random r = new Random();
            string path = "-1";
            int random = r.Next();

            if (picture != null && picture.ContentLength > 0)
            {
                string extension = Path.GetExtension(picture.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("~/Style/productImage"), random + Path.GetFileName(picture.FileName));
                        picture.SaveAs(path);
                        path = "~/Style/productImage/" + random + Path.GetFileName(picture.FileName);
                    }
                    catch (Exception ex)
                    {
                        path = "-2";
                    }

                }
                else
                {
                    Response.Write("<script>arlert('Only jpg , jpeg or png formats are acceptable.....');</script>");
                }
            }
            else
            {
                Response.Write("<script>arlert('Please select a file');</script>");
                path = "-3";
            }

            return path;
        }
        //Upload Image//////////////////


        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(product product , HttpPostedFileBase picture)
        {
            try
            {
                product pro = new product();
                string path = UpLoadImage(picture);

                pro.name = product.name;
                pro.categoryID = product.categoryID;
                pro.description = product.description;
                pro.price = product.price;
                pro.img = path;
                pro.isCombo = product.isCombo;
                pro.disable = product.disable;

                db.products.Add(pro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            using (OrderSystemEntities1 db = new OrderSystemEntities1())
            {
                SetViewBag();
                return View(db.products.Where(x => x.id == id).FirstOrDefault());
            }
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, product product, HttpPostedFileBase picture)
        {
            try
            {
                    string path = UpLoadImage(picture);
                    product.img = path;
                    
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            using (OrderSystemEntities1 db = new OrderSystemEntities1())
            {
              
                return View(db.products.Where(x => x.id == id).FirstOrDefault());
            }
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (OrderSystemEntities1 db = new OrderSystemEntities1())
                {

                    product product = db.products.Where(x => x.id == id).FirstOrDefault();
                    db.products.Remove(product);
                    db.SaveChanges();

                }

                    return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}