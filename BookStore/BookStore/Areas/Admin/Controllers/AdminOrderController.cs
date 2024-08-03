using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.DesignPattern.Command;
using BookStore.DesignPattern.Singleton;
using BookStore.Models;

namespace BookStore.Areas.Admin.Controllers
{
    public class AdminOrderController : Controller
    {
        //Singleton Design Pattern
        private readonly BookStoreEntities db = ModelManager.GetInstance().GetDbContext();

        // GET: Admin/AdminOrder
        public ActionResult Index()
        {
            ICommand command = new FetchOrderListCommand(db);
            command.Execute(this);
            return View();
        }

        public ActionResult Details(int id)
        {
            ICommand command = new FetchOrderDetailsCommand(db, id);
            command.Execute(this);
            return View();
        }

        public ActionResult Confirm(int id)
        {
            ICommand command = new FetchConfirmOrderCommand(db, id);      
            command.Execute(this);   
            return RedirectToAction("Index");
        }
    }
}