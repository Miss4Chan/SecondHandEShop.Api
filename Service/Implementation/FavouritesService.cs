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
    public class FavouritesService : IFavouritesService
    {

        private AppDbContext _context;
        public FavouritesService(AppDbContext context)
        {
            this._context = context;
        }
        public bool deleteProductFromFavourites(string email, int productId)
        {
            if (!string.IsNullOrEmpty(email) && productId != null)
            {
                var loggInUser = _context.ShopApplicationUsers.Where(u => u.Email == email)
                .Include(z => z.UserFavourites)
                .Include("UserFavourites.ProductsInFavourites")
                .Include("UserFavourites.ProductsInFavourites.Product")
                .FirstOrDefault();
                var userFavourites = loggInUser.UserFavourites;
                var itemToDelete = userFavourites.ProductsInFavourites.Where(z => z.ProductId.Equals(productId)).FirstOrDefault();
                userFavourites.ProductsInFavourites.Remove(itemToDelete);
                _context.Favourites.Update(userFavourites);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<ProductInFavourites> getFavouritesInfo(string email)
        {
            var loggInUser = _context.ShopApplicationUsers.Where(u => u.Email == email)
            .Include(z => z.UserFavourites)
            .Include("UserFavourites.ProductsInFavourites")
            .Include("UserFavourites.ProductsInFavourites.Product")
            .FirstOrDefault();

            var userFavourites = loggInUser.UserFavourites;
            var productsList = userFavourites.ProductsInFavourites.ToList();

            return productsList;
        }
    }
}
