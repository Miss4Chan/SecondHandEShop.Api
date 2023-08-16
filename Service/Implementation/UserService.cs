using Domain.CustomExceptions;
using Domain.Domain_models;
using Domain.DTO;
using Domain.Identity;
using Domain.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Interface;
using Service.Interface;
using System;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthenticatedUserDTO> SignIn(ShopApplicationUser user)
        {
            var dbUser = _userRepository.GetByEmail(user.Email);

            if (dbUser == null || _passwordHasher.VerifyHashedPassword(dbUser.Password, user.Password) == PasswordVerificationResult.Failed)
            {
                throw new InvalidEmailPasswordException("Invalid email or password");
            }

            return new AuthenticatedUserDTO()
            {
                Email = user.Email,
                Token = JwtGenerator.GenerateUserToken(user.Email)
            };
        }

        public async Task<AuthenticatedUserDTO> SignUp(ShopApplicationUser user)
        {
            var checkEmail = _userRepository.GetByEmail(user.Email);

            var checkUsername = _userRepository.GetByUsername(user.Username);

            if (checkUsername != null && checkUsername != null)
            {
                throw new EmailAlreadyExistsException("User with this Email or Username already exists!");
            }

            user.Password = _passwordHasher.HashPassword(user.Password);

            var userShoppingCart = new ShoppingCart
            {
                UserId = user.Id
            };

            user.UserShoppingCart = userShoppingCart;

            var userFavouries = new Favourites
            {
                UserId = user.Id
            };

            user.UserFavourites = userFavouries;
            user.UserRatingCount = 0;
            user.UserRating = 0;
            user.UserRatingTotal = 0;

            _userRepository.Insert(user);

            return new AuthenticatedUserDTO
            {
                Email = user.Email,
                Token = JwtGenerator.GenerateUserToken(user.Email)
        };
        }

        }
    }
