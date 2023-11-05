using Azure.Core;
using Brewery_App_Backend.Dbcontext;
using Brewery_App_Backend.Entities;
using Brewery_App_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Authentication;

namespace Brewery_App_Backend.Services
{
    public class AuthService : IAuthService
    {
        private BreweryContext _context;

        public AuthService(
            BreweryContext context)
        {
            _context = context;
        }


        // Login
        public async Task<Boolean> Authenticate(LoginRequest request)
        {
            var user = await _context.user.SingleOrDefaultAsync(x => x.Email == request.UserName);
            if (user == null)
            {
                throw new AuthenticationException("Email Or Password is Incorrect");
            }
            return true;
        }

        // Register User
        public async Task<Boolean> Register(RegisterRequest request)
        {
            var user = await _context.user.AnyAsync(x => x.UserName == request.UserName);
            if(user == null)
            {
                throw new BadHttpRequestException("Username already exists");
            }
            var newUser = new User();
            newUser.Email = request.Email;
            newUser.UserName = request.UserName;
            newUser.Password = request.Password;
            newUser.AuthId = new Guid();

             _context.user.Add(newUser);
            try
            {
                 await _context.SaveChangesAsync();
                 return true;
            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException("Something went wrong" + ex);
            }

        }
    }
}
