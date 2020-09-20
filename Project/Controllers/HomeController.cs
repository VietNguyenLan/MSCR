using Project.EF;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Home(DateTime? date, int? service_time, int? id)
        {
            if (date is null)
            {
                date = DateTime.Now.Date;
            }

            if (service_time is null)
            {
                service_time = 1;
            }
            if (id is null)
            {
                id = 1;
            }

            using (OrderSystemEntities2 db = new OrderSystemEntities2())

            {
                var date_menu = db.time_menu.SqlQuery("select * from time_menu where date_service ='" + date + "'").FirstOrDefault();
                int menuID = (int)date_menu.breakfast_mId;
                if (service_time == 2)
                {
                    menuID = (int)date_menu.lunch_mId;
                }
                if (service_time == 3)
                {
                    menuID = (int)date_menu.dinner_mId;
                }

                ViewBag.Dates = date;
                ViewBag.encode = EncodePassword("123123");

                List<product> productList = new List<product>();

                var menu_details = db.menu_detail.Where(x => x.menuId == menuID).ToList();
                foreach (menu_detail item in menu_details)
                {


                    product p = db.products.Where(x => x.id == item.productID).FirstOrDefault();
                    productList.Add(p);


                }


                return View(productList);
            }

        }

        public  string EncodePassword(string originalPassword)
        {
            //Declarations
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
            encodedBytes = md5.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes);
        }

        public ActionResult Add(int productID, DateTime date, int service_time)
        {

            product product = new product();
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                product = db.products.Where(x => x.id == productID).FirstOrDefault();
            }
            if (Session["cart"] == null)
            {
                AddNewCart(product, date, service_time);
            }
            else
            {
                List<CartItem> items = (List<CartItem>)Session["cart"];
                if (items.Count() == 0)
                {
                    items.Clear();
                    AddNewCart(product, date, service_time);
                }
                else if (!isCurrentCart(items, date, service_time))
                {
                    items.Clear();
                    AddNewCart(product, date, service_time);
                }
                else
                {
                    int index = isExist(items, product);
                    if (index == -1)
                    {
                        items.Add(new CartItem { Product = product, Quantity = 1, Date = date, ServiceTime = service_time, totalProduct = product.price });
                    }
                    else
                    {
                        items[index].Quantity++;
                        items[index].totalProduct = items[index].Product.price * items[index].Quantity;
                    }
                }

                Session["cart"] = items;
            }

            return RedirectToAction("Cart", "Cart");
        }



        public ActionResult Remove(product product)
        {

            List<CartItem> items = (List<CartItem>)Session["cart"];
            int index = isExist(items, product);
            items[index].Quantity--;
            items[index].totalProduct = items[index].Product.price * items[index].Quantity;
            if (items[index].Quantity == 0)
            {
                items.Remove(items[index]);
            }
            Session["cart"] = items;
            return RedirectToAction("");
        }

        private void AddNewCart(product product, DateTime date, int service_time)
        {
            List<CartItem> items = new List<CartItem>();
            CartItem item = new CartItem();
            item.Product = product;
            item.Quantity = 1;
            item.Date = date;
            item.ServiceTime = service_time;
            item.totalProduct = product.price;
            items.Add(item);
            Session["cart"] = items;
        }

        private bool isCurrentCart(List<CartItem> items, DateTime date, int service_time)
        {
            if (items[0].Date == date && items[0].ServiceTime == service_time)
            {
                return true;
            }
            return false;
        }

        private int isExist(List<CartItem> items, product product)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Product.id.Equals(product.id))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}