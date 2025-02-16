using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SafeVaultApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty, Required]
        public string Username { get; set; } = string.Empty;

        [BindProperty, Required]
        public string Password { get; set; } = string.Empty;

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _signInManager.PasswordSignInAsync(Username, Password, false, false);

            if (result.Succeeded)
            {
                return RedirectToPage("/Index");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }
    }
}
