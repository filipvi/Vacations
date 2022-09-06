using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Vacations.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class CheckEmailModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}