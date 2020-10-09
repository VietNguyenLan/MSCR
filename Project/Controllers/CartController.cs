using Project.EF;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Cart()
        {

            List<CartItem> list = (List <CartItem>) Session["cart"];
            if(list == null)
            {
                ViewBag.cartEmpty = 1;
                
                return View();

            }
            else
            {
                ViewBag.totalPrice = (int)list.Sum(x => x.totalProduct);
            
                return View(list);
            }
            
        }

        public ActionResult RemoveItem(int productID)
        {
            product product = new product();
            using(OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                product = db.products.Where(x => x.id == productID).FirstOrDefault();
            }
            List<CartItem> items = (List<CartItem>)Session["cart"];
            int index = isExist(items, product);
            items[index].Quantity--;
            
                items.Remove(items[index]);
            
            Session["cart"] = items;

            return RedirectToAction("Cart");
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
                        items.Add(new CartItem { Product = product, Quantity = 1, Date = date, ServiceTime = service_time });
                    }
                    else
                    {
                        items[index].Quantity++;
                        items[index].totalProduct = items[index].Product.price * items[index].Quantity;
                    }
                }

                Session["cart"] = items;
            }

            return RedirectToAction("Cart");
        }

        public ActionResult AddItem(int productID)
        {

            product product = new product();
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                product = db.products.Where(x => x.id == productID).FirstOrDefault();
            }
            List<CartItem> items = (List<CartItem>)Session["cart"];
            int index = isExist(items, product);
            items[index].Quantity++;
            items[index].totalProduct = items[index].Product.price * items[index].Quantity;


            Session["cart"] = items;

            return RedirectToAction("Cart");
        }

        public ActionResult Remove(int productID)
        {

            product product = new product();
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                product = db.products.Where(x => x.id == productID).FirstOrDefault();
            }
            List<CartItem> items = (List<CartItem>)Session["cart"];
            int index = isExist(items, product);
            items[index].Quantity--;
            items[index].totalProduct = items[index].Product.price * items[index].Quantity;
            if (items[index].Quantity == 0)
            {
                items.Remove(items[index]);

            }

            Session["cart"] = items;

            return RedirectToAction("Cart");
        }

        private void AddNewCart(product product, DateTime date, int service_time)
        {
            List<CartItem> items = new List<CartItem>();
            CartItem item = new CartItem();
            item.Product = product;
            item.Quantity = 1;
            item.Date = date;
            item.ServiceTime = service_time;
            item.totalProduct += product.price;
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