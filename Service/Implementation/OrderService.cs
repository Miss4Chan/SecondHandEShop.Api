using Domain.Domain_models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Implementation
{
    public class OrderService : IOrderService
    {
        private AppDbContext _context;
        public OrderService(AppDbContext context)
        {
            this._context = context;
        }

        public Order GetOrderDetails(int orderId)
        {
            return this._context.Orders.Include(z => z.User)
                     .Include(z => z.ProductsInOrder)
                     .Include("ProductsInOrder.Product").SingleOrDefault(z => z.Id == orderId);
        }

        public List<Order> GetAllOrders()
        {
            return this._context.Orders.Include(z => z.User)
                .Include(z => z.ProductsInOrder)
                .Include("ProductsInOrder.Product").ToList();
        }
        public List<Order> GetMyOrders(string username)
        {
            return this._context.Orders.Include(z => z.User).Where(u => u.User.Username == username)
                .Include(z => z.ProductsInOrder)
                .Include("ProductsInOrder.Product").ToList();
        }
    }
}
