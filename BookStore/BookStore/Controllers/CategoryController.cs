using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Controllers.Flyweight;
using BookStore.DesignPattern.Factory_Method;
using BookStore.DesignPattern.Singleton;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class CategoryController : Controller
    {
        //Singleton Design Pattern
        private BookStoreEntities db = ModelManager.GetInstance().GetDbContext();

        //Flyweight Design Pattern
        private readonly ProductFlyweightFactory flyweightFactory;

        //Factory Method Design Pattern
        private IProductFactory productFactory;

        public CategoryController()
        {
            flyweightFactory = new ProductFlyweightFactory();
            productFactory = new ProductFactory(db);
        }
        
        // GET: Category
        public ActionResult Index(int id)
        {
            var productsIndex = db.Products.Where(n => n.CategoryID == id).ToList();

            //factory method
            List<Product> products = new List<Product>();
            foreach (var item in productsIndex)
            {
                int productId = item.ProductID;
                products.Add(productFactory.CreateProduct(productId));
            }

            ViewBag.CategoryProd = db.Categories.FirstOrDefault(n => n.CategoryID == id);
            ViewBag.IdCategory = id;
            return View(productsIndex);
        }

        public ActionResult GetAllProduct()
        {
            var productGetAll = (from item in db.Products orderby item.ProductID descending select item).ToList();

            //flyweight 
            List<ProductFlyweight> flyweightProducts = new List<ProductFlyweight>();
            foreach (var product in productGetAll)
            {
                var flyweight = flyweightFactory.GetProductFlyweight(product.ProductID, product.ProductName, product.AuthorName, product.IntialPrice ?? 0, product.Price, product.CategoryID, product.Image, product.amount, product.ProductIntroduction);
                flyweightProducts.Add(flyweight);
            }
            return View(flyweightProducts);
        }

        public ActionResult Search(string searchString)
        {
            var result = db.Products.Where(s => s.ProductName.Contains(searchString)).ToList();
            return View(result);
        }
    }
}