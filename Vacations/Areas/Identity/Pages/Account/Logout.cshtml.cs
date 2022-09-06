using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Vacations.Core.Models.Identity;


namespace Vacations.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<Employee> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<Employee> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task OnGet()
        {
            // await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _signInManager.SignOutAsync();

        }
        

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            //await HttpContext.SignOutAsync(
            //    CookieAuthenticationDefaults.AuthenticationScheme);
            await _signInManager.SignOutAsync();
            // await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
