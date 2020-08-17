using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.EF;
using Project.Models;

namespace Project.AdminController
{
    public class IngredientsReportController : Controller
    {
        // GET: IngredientsReport
        public ActionResult Index()
        {
            using(OrderSystemEntities1 db = new OrderSystemEntities1())
            {
                DateTime date = DateTime.Now.Date;
                date = date.AddDays(1).Date;
                List<order> orders = db.orders.Where(x => x.take_date == date).ToList();
                List<order_detail> order_Details = new List<order_detail>();
                foreach (var order in orders)
                {
                    order_Details.AddRange(db.order_detail.Where(x => x.orderID == order.id).ToList());
                }
                List<product> products = new List<product>();
                foreach (var order_detail in order_Details)
                {
                    products.Add(order_detail.product);
                }
                List<product_ingresients> product_Ingresients = new List<product_ingresients>();
                foreach(product product in products)
                {
                    product_Ingresients.AddRange(db.product_ingresients.Where(x => x.productID == product.id).ToList());
                }
                List<IngredientDetail> ingredientDetails = new List<IngredientDetail>();
                foreach(product_ingresients pro_in in product_Ingresients)
                {
                    int index = isExist(pro_in.ingredient, ingredientDetails);
                    if(index == -1)
                    {
                        ingredientDetails.Add(new IngredientDetail()
                        {
                            Ingredient = pro_in.ingredient,
                            amount = pro_in.amount
                        });
                    }
                    else
                    {
                        ingredientDetails[index].amount += pro_in.amount;
                    }
                }

                return View(ingredientDetails);
            }
            
        }

        private int isExist(ingredient ing, List<IngredientDetail> list)
        {
            for(int i= 0; i< list.Count; i++)
            {
                if(list[i].Ingredient.id.Equals(ing.id))
                {
                    return i;
                }
            }   

            return -1;
        }
    }
}