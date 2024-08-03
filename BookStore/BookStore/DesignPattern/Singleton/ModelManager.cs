using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Models;

namespace BookStore.DesignPattern.Singleton
{
    //All Controller
    public class ModelManager
    {
        private static ModelManager instance;
        private static readonly object lockObject = new object();
        private BookStoreEntities db;

        private ModelManager()
        {
            db = new BookStoreEntities();
        }

        public static ModelManager GetInstance()
        {
            lock (lockObject)
            {
                if (instance == null)
                {
                    instance = new ModelManager();
                }
                return instance;
            }
        }

        public BookStoreEntities GetDbContext()
        {
            return db;
        }
    }
}