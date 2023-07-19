using Domain.DTO;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service.Interface;
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

            var loggedInUser2 = _context.ShopApplicationUsers.Where(u => u.Email == email).FirstOrDefault();
            var mail = email;
            var loggedInUser = _context.ShopApplicationUsers.Where(u => u.Email == email)
                .Include(z => z.UserShoppingCart)
              .Include("UserShoppingCart.ProductsInShoppingCart")
              .Include("UserShoppingCart.ProductsInShoppingCart.Product")
              .FirstOrDefault();

            var userCart = loggedInUser.UserShoppingCart;

            var productsList = userCart.ProductsInShoppingCart.ToList();

          /* var ticketPrices = ticketsList.Select(z => new
            {
                TicketPrice = z.Ticket.Price,
            }).ToList(); */

            double totalPrice = 0.0;

           /* foreach (var item in ticketPrices)
            {
                totalPrice += item.Quantity * item.TicketPrice;
            }
           */
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
    }
}
