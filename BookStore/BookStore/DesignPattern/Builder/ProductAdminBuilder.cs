using BookStore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BookStore.DesignPattern.Builder
{
    public class ProductAdminBuilder
    {
        private Product _product;

        public ProductAdminBuilder(Product product)
        {
            _product = product;
        }

        public ProductAdminBuilder SetImage(HttpPostedFileBase ImgProd1)
        {
            if (ImgProd1 != null)
            {
                var fileName = Path.GetFileName(ImgProd1.FileName);
                var path = Path.Combine(HttpContext.Current.Server.MapPath("~/image"), fileName);
                _product.Image = fileName;
                ImgProd1.SaveAs(path);
            }
            return this;
        }

        public Product Build()
        {
            return _product;
        }
    }
}