using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AnnoBibLibrary.Exceptions;
using AnnoBibLibrary.Models;
using AnnoBibLibrary.RouteModels;
using AnnoBibLibrary.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AnnoBibLibrary.Repos
{
    public class DbUserRepo : IUserRepo
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public DbUserRepo(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> CreateUser(NewUser newUser)
        {
            var user = new ApplicationUser 
            { 
                UserName = newUser.Email, 
                Email = newUser.Email,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName
            };

            var createResult = await _userManager.CreateAsync(user, newUser.Password);

            if(createResult.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                return code;
            } 

            return null;
        }

        public async Task<UserInfo> Authenticate(Credentials credentials)
        {
            var user = await _userManager.FindByEmailAsync(credentials.Email);

            if(!await _userManager.CheckPasswordAsync(user, credentials.Password) || user == null)
                throw new EmailOrPasswordNotMatchedException();

            var token = GenerateJwtToken(user);
            return new UserInfo(user) { Token = token };
        }

        public async Task<ApplicationUser> GetById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            // generate token that is valid for 14 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("Secret"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(14),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}