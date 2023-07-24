using Domain.Domain_models;
using Domain.DTO;
using Domain.Enums;
using Domain.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service.Interface;
using System;
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
            product.ProductType = (ProductType)Enum.Parse(typeof(ProductType), productDTO.ProductType);
            product.ProductSizeNumber = productDTO.ProductSizeNumber;
            product.ProductPrice = productDTO.ProductPrice;
            product.ProductAvailablity = productDTO.ProductAvailablity;
            product.ProductColor = productDTO.ProductColor;
            product.ProductMeasurements = productDTO.ProductMeasurements;
            product.ProductSize = Enum.TryParse(productDTO.ProductSize, out Size size) ? size : (Size?)null;
            product.ProductSubcategory = Enum.TryParse(productDTO.ProductSubcategory, out ClothingSubcategory clothing) ? clothing : (ClothingSubcategory?)null;

            _context.SaveChanges();

            return productDTO;
        }

        public List<ProductDTO> GetAllProducts()
        {
            return _context.Products
                .Select(p => (ProductDTO)p)
                .ToList();
        }

        public List<ProductDTO> GetMyProducts()
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

        public bool AddToShoppingCart(Product product, string email)
        {
            var user = _context.ShopApplicationUsers
                .Include(u => u.UserShoppingCart) // Eagerly load the UserShoppingCart navigation property
                .ThenInclude(u => u.ProductsInShoppingCart)
                .FirstOrDefault(user => user.Email == email);

            var userShoppingCart = user.UserShoppingCart;

            if (userShoppingCart != null && product != null)
            {
                var isAlreadyAdded = userShoppingCart.ProductsInShoppingCart.FirstOrDefault(p => p.ProductId == product.Id);

                if (isAlreadyAdded == null)
                {
                    _context.Attach(product);
                    var productInShoppingCart = new ProductInShoppingCart
                    {
                        ShoppingCart = userShoppingCart,
                        Product = product,
                        ShoppingCartId = userShoppingCart.Id,
                        ProductId = product.Id
                    };

                    _context.ProductsInShoppingCarts.Add(productInShoppingCart);
                    _context.SaveChanges();
                }
                return true;
            }

            return false;
        }

    }
}
