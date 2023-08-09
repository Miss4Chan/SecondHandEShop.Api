using Domain.Domain_models;
using Domain.DTO;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {

        private AppDbContext _context;
        public ShoppingCartService(AppDbContext context)
        {
            this._context = context;
        }
        public ShoppingCartDTO getShoppingCartInfo(string email)
        {
            var loggedInUser = _context.ShopApplicationUsers.Where(u => u.Email == email)
                .Include(z => z.UserShoppingCart)
              .Include("UserShoppingCart.ProductsInShoppingCart")
              .Include("UserShoppingCart.ProductsInShoppingCart.Product")
              .FirstOrDefault();

            var userCart = loggedInUser.UserShoppingCart;

            var productsList = userCart.ProductsInShoppingCart.ToList();

            double totalPrice = 0.0;

            foreach (var item in productsList)
            {
                totalPrice += item.Product.ProductPrice;
            }

            var result = new ShoppingCartDTO
            {
                ProductsInShoppingCart = productsList,
                TotalPrice = totalPrice
            };

            return result;
        }

        public bool deleteProductFromShoppingCart(string email, int productId)
        {
            if (!string.IsNullOrEmpty(email) && productId != null)
            {
                var loggInUser = _context.ShopApplicationUsers.Where(u => u.Email == email)
                .Include(z => z.UserShoppingCart)
                .Include("UserShoppingCart.ProductsInShoppingCart")
                .Include("UserShoppingCart.ProductsInShoppingCart.Product")
                .FirstOrDefault();
                var userShoppingCart = loggInUser.UserShoppingCart;
                var itemToDelete = userShoppingCart.ProductsInShoppingCart.Where(z => z.ProductId.Equals(productId)).FirstOrDefault();
                userShoppingCart.ProductsInShoppingCart.Remove(itemToDelete);
                _context.ShoppingCarts.Update(userShoppingCart);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Order OrderNow(string email, string deliveryType, string deliveryAddress, string deliveryPhone)
        {
            var loggedInUser = _context.ShopApplicationUsers.Where(u => u.Email == email)
            .Include(z => z.UserShoppingCart)
            .Include("UserShoppingCart.ProductsInShoppingCart")
            .Include("UserShoppingCart.ProductsInShoppingCart.Product")
             .FirstOrDefault();
            var userCard = loggedInUser.UserShoppingCart;

            Order order = new Order
            {
                User = loggedInUser,
                UserId = loggedInUser.Id,
                DeliveryType = (DeliveryType)Enum.Parse(typeof(DeliveryType), deliveryType),
                DeliveryAddress = deliveryAddress,
                DeliveryPhone = deliveryPhone
        };

            this._context.Orders.Add(order);

            List<ProductInOrder> productsInOrder = new List<ProductInOrder>();

            var result = userCard.ProductsInShoppingCart.Select(z => new ProductInOrder
            {
                ProductId = z.ProductId,
                Product = z.Product,
                OrderId = order.Id,
                Order = order
            }).ToList();

            productsInOrder.AddRange(result);

            foreach (var item in productsInOrder)
            {
                this._context.ProductsInOrders.Add(item);
                var product = item.Product;
                product.ProductAvailablity = false;
                this._context.Products.Update(product);
            }

            loggedInUser.UserShoppingCart.ProductsInShoppingCart.Clear();

            this._context.ShopApplicationUsers.Update(loggedInUser);
            this._context.SaveChanges();
            return order;

        }
    }
}

