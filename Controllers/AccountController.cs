using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AnnoBibLibrary.Exceptions;
using AnnoBibLibrary.Models;
using AnnoBibLibrary.Repos;
using AnnoBibLibrary.RouteModels;
using AnnoBibLibrary.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AnnoBibLibrary.Controllers
{
    [Route("accounts")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly IUserRepo _userRepo;

        public AccountController (
            ILogger<AccountController> logger,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IUserRepo userRepo
        ) {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userRepo = userRepo;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(NewUser newUser)
        {
            if(ModelState.IsValid)
            {
                var confirmCode = await _userRepo.CreateUser(newUser);

                if(confirmCode != null)
                    return Ok(confirmCode);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("confirm")]
        public async Task<IActionResult> ConfirmEmail(string email, string code)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var confirmResult = await _userManager.ConfirmEmailAsync(
                    user, 
                    Encoding.Default.GetString(WebEncoders.Base64UrlDecode(code))
                );

            if(confirmResult.Succeeded)
            {
                return Ok();
            }
            else
            {
                foreach(var error in confirmResult.Errors)
                ModelState.AddModelError("", error.Description);

                return BadRequest(ModelState);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]Credentials credentials)
        {
            try
            {
                var response = await _userRepo.Authenticate(credentials);
                Response.Cookies.Append("JWT", response.Token, 
                    new CookieOptions 
                    { 
                        IsEssential = true, 
                        Expires = DateTime.Now.AddDays(14),  
                        HttpOnly = true
                    });
                return Ok(response); 
            }
            catch(EmailOrPasswordNotMatchedException)
            {
                return BadRequest("Email or password did not match");
            }
        }

        [Authorize]
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            return Ok();
        }
    }
}