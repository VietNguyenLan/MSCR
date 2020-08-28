using Project.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class ProductDetailController : Controller
    {
        // GET: ProductDetail
        public ActionResult Index(int productID)
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                product product = db.products.Where(x => x.id == productID).FirstOrDefault();
                List<product_ingresients> product_Ingresients = db.product_ingresients.Where(x => x.productID == productID).ToList();
                List<ingredient> ingredients = new List<ingredient>();
                foreach (var item in product_Ingresients)
                {
                    ingredients.Add(item.ingredient);
                }
                ViewBag.product = product;
                ViewBag.listIng = ingredients;
                return View();
            }

        }
    }
}