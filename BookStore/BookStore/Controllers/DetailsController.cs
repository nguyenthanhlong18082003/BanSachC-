using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using BookStore.Models;
using System.Data.Entity;
using BookStore.DesignPattern.Facade;
using BookStore.DesignPattern.Singleton;

namespace BookStore.Controllers
{
    public class DetailsController : Controller
    {
        //Singleton Design Pattern
        private readonly BookStoreEntities db = ModelManager.GetInstance().GetDbContext();

        private readonly DetailsFacade _facade;
        public DetailsController()
        {
            _facade = new DetailsFacade(db);
        }

        // GET: Details
        public ActionResult Index(int id)
        {
            ViewBag.ProdDetails = _facade.GetProductById(id);
            ViewBag.ThisProdCategories = _facade.GetCategoryById(id);
            ViewBag.ProductList = _facade.GetRelatedProducts(id);
            ViewBag.CommentList = _facade.GetCommentsForProduct(id);

            return View();
        }

        [HttpPost]
        public ActionResult AddComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                _facade.AddComment(comment);
            }
            int productId = comment.ProductID.GetValueOrDefault();
            return RedirectToAction("Index/" + productId, "Details");
        }

        public ActionResult DeleteComment(int id)
        {
            int? productId = _facade.GetProductIdOfComment(id);
            if (productId.HasValue)
            {
                _facade.DeleteComment(id);
                return RedirectToAction("Index/" + productId.Value, "Details");
            }
            return HttpNotFound();
        }
    }
}