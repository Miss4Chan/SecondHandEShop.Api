using Domain.Domain_models;
using Domain.DTO;
using Domain.Enums;
using Domain.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
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

        public List<ProductDTO> GetProducts(string searchTerm, string colorFilter, string sizeFilter, string conditionFilter, string sortByPrice)
        {
            var products = _context.Products.ToList();

            // Filter by productName
            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(p => p.ProductName.ToLower().Contains(searchTerm.ToLower())).ToList();
            }

            // Filter by color
            if (!string.IsNullOrEmpty(colorFilter))
            {
                products = products.Where(p => p.ProductColor == colorFilter).ToList();
            }

            // Filter by size
            if (!string.IsNullOrEmpty(sizeFilter) && Enum.TryParse<Size>(sizeFilter, out var size))
            {
                products = products.Where(p => p.ProductSize == size).ToList();
            }


            // Filter by condition
            if (!string.IsNullOrEmpty(conditionFilter) && Enum.TryParse<Condition>(conditionFilter, out var condition))
            {
                products = products.Where(p => p.Condition == condition).ToList();
            }

            // Sort by price (ascending order)
            if (sortByPrice?.ToLower() == "desc")
            {
                products = products.OrderByDescending(p => p.ProductPrice).ToList();
            }
            else if (sortByPrice?.ToLower() == "asc")
            {
                products = products.OrderBy(p => p.ProductPrice).ToList();
            }

            var productsDTO = products.Select(p => (ProductDTO)p).ToList();
            return productsDTO;
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

        public bool AddToFavourites(Product product, string email)
        {
            var user = _context.ShopApplicationUsers
                .Include(u => u.UserFavourites) 
                .ThenInclude(u => u.ProductsInFavourites)
                .FirstOrDefault(user => user.Email == email);

            var userFavourites = user.UserFavourites;

            if (userFavourites != null && product != null)
            {
                var isAlreadyAdded = userFavourites.ProductsInFavourites.FirstOrDefault(p => p.ProductId == product.Id);

                if (isAlreadyAdded == null)
                {
                    _context.Attach(product);
                    var productInFavourites = new ProductInFavourites
                    {
                        Favourites = userFavourites,
                        Product = product,
                        FavouritesId = userFavourites.Id,
                        ProductId = product.Id
                    };

                    _context.ProductsInFavourites.Add(productInFavourites);
                    _context.SaveChanges();
                }
                return true;
            }

            return false;
        }

    }
}
