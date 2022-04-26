using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace OnlineShop.UI.Pages.Accounts
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public void OnGet()
        {
        }
        [BindProperty]
        public LoginViewModel Input { get; set; }
        public async Task<IActionResult> OnPost()
        {

            try
            {
                var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToPage("/Admin/Index");
                }
                else
                {
                    return Page();
                }
            }
            catch (ArgumentNullException)
            {
                return Page();
            }
        }
    }
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
