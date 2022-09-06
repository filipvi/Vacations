using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vacations.Core.Models.Identity;

namespace Vacations.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class UnconfirmedEmailModel : PageModel
    {
        private readonly UserManager<Employee> _userManager;

        public UnconfirmedEmailModel(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }

        [TempData]
        public string UserId { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Obavezno")]
            [EmailAddress(ErrorMessage = "Krivi format")]
            public string Email { get; set; }
        }

        public async Task OnGet(string userId)
        {
            UserId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            Input.Email = user.Email;
            ModelState.Clear();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(UserId);

                if (user == null)
                {
                    // Don't reveal that the user does not exist
                    return RedirectToPage("./CheckEmail");
                }

                if (user.Email != Input.Email)
                {
                    if (_userManager.Options.User.RequireUniqueEmail)
                    {
                        var owner = await _userManager.FindByEmailAsync(Input.Email);
                        if (owner != null && !string.Equals
                           (await _userManager.GetUserIdAsync(owner),
                            await _userManager.GetUserIdAsync(user)))
                        {
                            ModelState.AddModelError(string.Empty,
                            new IdentityErrorDescriber().DuplicateEmail(Input.Email).Description);
                            return Page();
                        }
                    }

                    await _userManager.SetEmailAsync(user, Input.Email);
                }

                var result = await _userManager.UpdateSecurityStampAsync(user);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                        return Page();
                    }
                }

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { userId = user.Id, code },
                    protocol: Request.Scheme);

                //await _emailSender.SendEmailAsync(Input.Email, "Potvrda email adresa",
                //    $"Molimo Vas da potvrdite Vašu email adresu <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>klikom na ovaj link</a>.");

                return RedirectToPage("./CheckEmail");
            }

            return Page();
        }
    }
}