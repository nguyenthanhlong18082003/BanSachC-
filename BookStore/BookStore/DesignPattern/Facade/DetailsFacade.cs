using BookStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Web;

namespace BookStore.DesignPattern.Facade
{
    //Detail Controller
    public class DetailsFacade
    {
        private readonly BookStoreEntities _db;

        public DetailsFacade(BookStoreEntities db)
        {
            _db = db;
        }

        public Product GetProductById(int id)
        {
            return _db.Products.FirstOrDefault(n => n.ProductID == id);
        }

        public Category GetCategoryById(int id)
        {
            int thisProdCategories = GetProductById(id).CategoryID;
            return _db.Categories.FirstOrDefault(n => n.CategoryID == thisProdCategories);
        }

        public IEnumerable<Product> GetRelatedProducts(int productId)
        {
            int thisProdCategories = GetProductById(productId).CategoryID;
            return (from p in _db.Products where p.CategoryID == thisProdCategories && p.ProductID != productId select p).Take(10).ToList();
        }

        public IEnumerable<Comment> GetCommentsForProduct(int productId)
        {
            return (from c in _db.Comments orderby c.id descending where c.ProductID == productId select c).ToList();
        }

        public void AddComment(Comment comment)
        {
            _db.Comments.Add(comment);
            _db.SaveChanges();
        }

        public Comment GetComment(int id)
        {
            return _db.Comments.FirstOrDefault(c => c.id == id);
        }

        public int? GetProductIdOfComment(int id)
        {
            var comment = GetComment(id);
            return comment?.ProductID;
        }

        public void DeleteComment(int id)
        {
            var comment = GetComment(id);
            if (comment != null)
            {
                _db.Comments.Remove(comment);
                _db.SaveChanges();
            }
        }
    }
}