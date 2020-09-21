using Project.EF;
using Project.Security;
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
        OrderSystemEntities2 db = new OrderSystemEntities2();
        // GET: Product
        [DeatAuthorize(Order = 3)]
        public ActionResult Index()
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                SetViewBag();
                return View(db.products.Include(c => c.category).ToList());
            }
        }

        public ActionResult Product_By_Category(int categoryID)
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                return View(db.products.Include(c => c.category).Where(a => a.categoryID == categoryID).ToList());
            }
        }

        public ActionResult Product_By_Name_Search(string pname)
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                return View(db.products.SqlQuery("SELECT * FROM PRODUCT WHERE NAME LIKE '%"+ pname + "%'"+ "or description like '%"+pname+"%' or price like '%"+ pname +"%'").ToList());
            }
        }

        // GET: Product/Details/5

        public ActionResult Details(int id)
        {
            using(OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                return View(db.products.Where(x => x.id == id).FirstOrDefault());
            }
           
        }
        [DeatAuthorize(Order = 3)]
        public ActionResult Details_Ingredient(int id)
        {
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
            {
                return View(db.product_ingresients.Include(a => a.product).Include(b => b.ingredient).Where(x => x.productID == id).ToList());
            }

        }
   
        public ActionResult Create_Ingredient()
        {
            try
            {
                using (OrderSystemEntities2 db = new OrderSystemEntities2())
                {
                    SetViewBagProduct();
                    SetViewBagIngredient();

                }
            }
            catch
            {

            }
            return View();

        }
    
        [HttpPost]
        public ActionResult Create_Ingredient(product_ingresients product_Ingresients)
        {
            try
            {
                using (OrderSystemEntities2 db = new OrderSystemEntities2())
                {

                    db.product_ingresients.Add(product_Ingresients);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


  
        // GET: Product/Create
        public ActionResult Create()
        {
            try
            {
                using(OrderSystemEntities2 db = new OrderSystemEntities2())
                {

                    SetViewBag();
                    
                }
            }
            catch
            {

            }
            return View();
        }



    
        //product
        public void SetViewBagProduct(long? productID = null)
        {
            ViewBag.productID = new SelectList(ListAllpro(), "id", "name", productID);
        }
        public List<product> ListAllpro()
        {
            return db.products.Where(x => x.disable == false).ToList();
        }

        //get list category
        public void SetViewBag(long? categoryID = null )
        {
            ViewBag.categoryID = new SelectList(ListAll(), "id", "name" , categoryID);
        }
        public List<category> ListAll()
        {
            return db.categories.Where(x => x.disable == false).ToList();
        }


        //get list category
        public void SetViewBagIngredient(long? ingID = null)
        {
            ViewBag.ingID = new SelectList(ListAllIngredients(), "id", "name" , ingID);
        }
        public List<ingredient> ListAllIngredients()
        {
            return db.ingredients.Where(x => x.disable == false).ToList();
        }
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
                path = "~/Style/productImage/2054211503bun-dau-mam-tom-thap-cam.jpg";
            }

            return path;
        }
        //Upload Image//////////////////

     
        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(product product , HttpPostedFileBase picture ,product_ingresients product_Ingresients)
        {
            OrderSystemEntities2 db1 = new OrderSystemEntities2();
            try
            {
                product pro = new product();
                product_ingresients pro_in = new product_ingresients();

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



                //int lastProductId = db.products.Max(item => item.id);
                ////int lastProductId1 = pro.id;

                //pro_in.productID = lastProductId;
                //pro_in.ingID = product_Ingresients.ingID;
                //pro_in.amount = product.amount;


                //db1.product_ingresients.Add(pro_in);

                //db1.SaveChanges();

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
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
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
            using (OrderSystemEntities2 db = new OrderSystemEntities2())
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
                using (OrderSystemEntities2 db = new OrderSystemEntities2())
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
