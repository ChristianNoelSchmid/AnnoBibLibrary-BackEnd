using System.Security.Claims;
using System.Threading.Tasks;
using AnnoBibLibrary.Exceptions;
using AnnoBibLibrary.Models;
using AnnoBibLibrary.RouteModels;
using Microsoft.AspNetCore.Identity;

namespace AnnoBibLibrary.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;
        

        public AuthenticationService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
        ) {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> ValidateLoginAsync(Credentials credentials)
        {
            var user = await _userManager.FindByEmailAsync(credentials.Email);

            if(user == null)
                throw new AccountNotFoundException();

            return await _userManager.CheckPasswordAsync(user, credentials.Password);
        }

        /// <summary>
        /// Get's the principal for the ApplicationUser account,
        /// In order to store information in cookie.
        /// Does NOT validate information beforehand (use with ValidateLogin)
        /// </summary>
        public async Task<ClaimsPrincipal> GetPrincipalAsync(string email) 
        {
            var user = await _userManager.FindByEmailAsync(email);

            if(user == null)
                throw new AccountNotFoundException();

            return await _signInManager.CreateUserPrincipalAsync(user);
        }
    }
}