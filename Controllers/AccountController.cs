using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace SafeVaultApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(username);
                if (user != null)
                {
                    var authToken = await _userManager.GenerateUserTokenAsync(user, "Default", "token");
                    Response.Headers.Append("Set-Cookie", $"AuthToken={authToken}; Path=/; HttpOnly");
                    return Ok("Login successful");
                }
            }
            return BadRequest("Invalid login attempt");
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string email, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                return BadRequest("Passwords do not match");
            }

            var user = new IdentityUser { UserName = username, Email = email };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok("Registration successful");
            }

            return BadRequest("Registration failed");
        }
    }
}
