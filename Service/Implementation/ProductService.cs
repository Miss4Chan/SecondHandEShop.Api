using Domain.Domain_models;
using Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Implementation
{
    public class ProductService : IProductService
    {
        private AppDbContext _context;
        public ProductService (AppDbContext context)
        {
            this._context = context;
        }

        public Product CreateProduct(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
            return product;
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public Product EditProduct(Product product)
        {
            var p = _context.Products.First(p => p.Id == product.Id);
            p.ProductDescription = product.ProductDescription;
            p.ProductName = product.ProductName;
            _context.SaveChanges();

            return p;
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProduct(int id)
        {
            return _context.Products.First(p=>p.Id==id);
        }
    }
}
