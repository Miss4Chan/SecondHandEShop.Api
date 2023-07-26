using Domain.DTO;
using Domain.Identity;
using Microsoft.AspNetCore.Http;
using Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Implementation
{
    public class UserProfileService : IUserProfileService
    {
        private AppDbContext _context;
        private readonly ShopApplicationUser _user;
        public UserProfileService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this._context = context;
            _user = _context.ShopApplicationUsers.First(u => u.Email == httpContextAccessor.HttpContext.User.Identity.Name);
        }
        public UserDTO GetMyProfile()
        {

            UserDTO userDTO = new UserDTO
            {
                Name = _user.Name,
                Surname = _user.Surname,
                Phone = _user.Phone,
                Address = _user.Address,
                Email = _user.Email,
                Username = _user.Username
            };

            return userDTO;
        }
        public UserDTO GetProfile(string username)
        {
            var user = _context.ShopApplicationUsers.FirstOrDefault(u => u.Username == username);
            var productList = _context.Products.Where(p => p.ShopApplicationUser.Id == user.Id).Select(p => (ProductDTO)p).ToList(); ;


            if (user != null)
            {
                UserDTO userDTO = new UserDTO
                {
                    Name = user.Name,
                    Surname = user.Surname,
                    Phone = user.Phone,
                    Address = user.Address,
                    Email = user.Email,
                    Username = user.Username,
                    Products = productList
                };

                return userDTO;
            }

            return null;

        }
    }
}
