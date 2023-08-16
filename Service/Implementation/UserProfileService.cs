using Domain.Domain_models;
using Domain.DTO;
using Domain.Identity;
using Microsoft.AspNetCore.Http;
using Repository;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Implementation
{
    public class UserProfileService : IUserProfileService
    {
        public readonly IUserRepository _userRepository;
        public readonly ICommentRepository _commentRepository;
        public readonly IProductRepository _productRepository;
        public readonly ShopApplicationUser _user;

        public UserProfileService (IHttpContextAccessor httpContextAccessor, IUserRepository userRepository, ICommentRepository commentRepository, IProductRepository productRepository, AppDbContext _context)
        {
            this._userRepository = userRepository;
            this._commentRepository = commentRepository;
            this._productRepository = productRepository;
            this._user = _userRepository.GetByEmail(httpContextAccessor.HttpContext.User.Identity.Name);

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
                Username = _user.Username,
                City = _user.City,
                PostalCode = _user.PostalCode
            };

            return userDTO;
        }
        public UserDTO GetProfile(string username)
        {
            var user = this._userRepository.GetByUsername(username);
            var productList = _productRepository.GetProductsByEmail(user.Email);
            var comments = this._commentRepository.GetByReceiver(user.Id);


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
                    Products = productList,
                    Rating = user.UserRating,
                    RatingCount = user.UserRatingCount,
                    Comments = comments
                };

                return userDTO;
            }

            return null;

        }
    }
}
