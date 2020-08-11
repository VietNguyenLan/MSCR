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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(product product, DateTime date, int service_time)
        {
 
            if(Session["cart"] == null)
            {
                AddNewCart(product, date, service_time);
            }
            else
            {
                List<CartItem> items = (List<CartItem>)Session["cart"];
                if(!isCurrentCart(items, date, service_time))
                {
                    items.Clear();
                    AddNewCart(product, date, service_time);
                }
                else
                {
                    int index = isExist(items, product);
                    if(index == -1)
                    {
                        items.Add(new CartItem { Product = product, Quantity = 1, Date = date, ServiceTime = service_time });
                    }
                    else
                    {
                        items[index].Quantity++;
                    }
                }

                Session["cart"] = items;
            }

            return RedirectToAction("");
        }

        public ActionResult Remove(product product)
        {
            List<CartItem> items = (List<CartItem>)Session["cart"];
            int index = isExist(items, product);
            items[index].Quantity--;
            if(items[index].Quantity == 0)
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
            items.Add(item);
            Session["cart"] = items;
        }

        private bool isCurrentCart(List<CartItem> items, DateTime date, int service_time)
        {
            if(items[1].Date == date && items[1].ServiceTime == service_time)
            {
                return true;
            }
            return false;
        }

        private int isExist(List<CartItem> items, product product)
        {
            for(int i = 0; i< items.Count; i++)
            {
                if (items[i].Product.id.Equals(product.id)){
                    return i;
                }
            }

            return -1;
        }
    }
}