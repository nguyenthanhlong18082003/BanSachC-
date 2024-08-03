using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.DesignPattern.Singleton;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class MenuNavPartialController : Controller
    {
        //Singleton Design Pattern
        BookStoreEntities db = ModelManager.GetInstance().GetDbContext();

        // GET: MenuNavPartial
        public ActionResult MenuNav()
        {
            ViewBag.CategoryNavList = db.Categories.ToList();
            return PartialView();
        }
    }
}