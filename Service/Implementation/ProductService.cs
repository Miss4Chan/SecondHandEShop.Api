﻿using Domain.Domain_models;
using Domain.DTO;
using Domain.Identity;
using Microsoft.AspNetCore.Http;
using Repository;
using Service.Interface;
using System.Collections.Generic;
using System.Linq;


namespace Service.Implementation
{
    public class ProductService : IProductService
    {
        private AppDbContext _context;
        private readonly ShopApplicationUser _user;
        public ProductService (AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this._context = context;
            var _users = _context.ShopApplicationUsers.ToArray();
            var name = httpContextAccessor.HttpContext.User.Identity.Name;
            _user = _context.ShopApplicationUsers.First(u => u.Email == httpContextAccessor.HttpContext.User.Identity.Name);
            //httpContextAccessor.HttpContext.User.Identity.Name --> The name we have inside the JWT Token
        }

        public ProductDTO CreateProduct(Product product)
        {
            product.ShopApplicationUser = _user;
            _context.Add(product);
            _context.SaveChanges();
            return (ProductDTO) product;
        }

        public void DeleteProduct(ProductDTO productDTO)
        {
            var product = _context.Products.First(p => p.ShopApplicationUser.Id == _user.Id && p.Id == productDTO.Id);
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public ProductDTO EditProduct(ProductDTO productDTO)
        {
            var product = _context.Products.First(p => p.ShopApplicationUser.Id == _user.Id && p.Id == productDTO.Id);
            product.ProductDescription = productDTO.ProductDescription;
            product.ProductName = productDTO.ProductName;
            _context.SaveChanges();

            return productDTO;
        }

        public List<ProductDTO> GetAllProducts()
        {
            return _context.Products
                .Where(p => p.ShopApplicationUser.Id == _user.Id)
                .Select(p => (ProductDTO)p)
                .ToList();
        }

        public ProductDTO GetProduct(int id)
        {
            return _context.Products
                .Where(p => p.ShopApplicationUser.Id == _user.Id && p.Id == id)
                .Select(p => (ProductDTO)p)
                .First();
               
        }
    }
}