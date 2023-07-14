using Domain.CustomExceptions;
using Domain.DTO;
using Domain.Identity;
using Domain.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service.Interface;
using System;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(AppDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthenticatedUserDTO> SignIn(ShopApplicationUser user)
        {
            var dbUser = await _context.ShopApplicationUsers
                .FirstOrDefaultAsync(u => u.Email == user.Email);

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
            var checkUsername = await _context.ShopApplicationUsers
                    .FirstOrDefaultAsync(u => u.Email.Equals(user.Email));

            if (checkUsername != null)
            {
                throw new EmailAlreadyExistsException("User with this Email already exists!");
            }

            user.Email = user.Email;
            user.Password = _passwordHasher.HashPassword(user.Password);
            user.Name = user.Name;
            user.Surname = user.Surname;
            user.Phone = user.Phone;
            user.Address = user.Address;

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return new AuthenticatedUserDTO
            {
                Email = user.Email,
                Token = JwtGenerator.GenerateUserToken(user.Email)
        };
        }

        }
    }
