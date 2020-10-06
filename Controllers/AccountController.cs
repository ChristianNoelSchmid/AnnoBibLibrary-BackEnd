using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AnnoBibLibrary.Exceptions;
using AnnoBibLibrary.Models;
using AnnoBibLibrary.RouteModels;
using AnnoBibLibrary.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
        private readonly Services.IAuthenticationService _authorizationService;

        public AccountController (
            ILogger<AccountController> logger,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            Services.IAuthenticationService authorizationService
        ) {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _authorizationService = authorizationService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(Credentials credentials)
        {
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = credentials.Email, Email = credentials.Email };
                var createResult = await _userManager.CreateAsync(user, credentials.Password);

                if(createResult.Succeeded)
                {
                    _logger.LogInformation("Create new user with email " + credentials.Email); 

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    return Ok(code);
                } 
                else
                {
                    foreach(var error in createResult.Errors)
                        ModelState.AddModelError("", error.Description);
                }
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
            _logger.LogInformation(credentials.ToString());

            try
            {
                // We will typically move the validation of credentials
                // and return of matched principal into its own
                // AuthenticationService
                // Leaving it here for convenience (change later)
                if(!await _authorizationService.ValidateLoginAsync(credentials))
                {
                    return BadRequest("Account email or password not found.");
                }

                var user = await _userManager.FindByEmailAsync(credentials.Email);

                await _signInManager.SignInAsync(user, new AuthenticationProperties{
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(14)
                });

                return Ok(JsonConvert.SerializeObject(new UserInfo(user)));
            }
            catch(AccountNotFoundException)
            {
                return BadRequest("Account was not found.");
            }
        }

        [HttpPost("trylogin")]
        [Authorize]
        public async Task<IActionResult> TryLogin()
        {
            var email = HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(email);

            return Ok(JsonConvert.SerializeObject(new UserInfo(user)));
        }

        [HttpGet("logout"), Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}