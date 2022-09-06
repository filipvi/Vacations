using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Vacations.Core.Models.Identity;

namespace Vacations.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly ILogger<RegisterModel> _logger;
        private readonly SignInManager<Employee> _signInManager;
        private readonly UserManager<Employee> _userManager;

        public RegisterModel(UserManager<Employee> userManager, SignInManager<Employee> signInManager, ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty] public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }
        
        public class InputModel
        {
            [Display(Name = "First name")]
            [Required(ErrorMessage = "Required")]
            public string FirstName { get; set; }

            [Display(Name = "Last name")]
            [Required(ErrorMessage = "Required")]
            public string LastName { get; set; }

            [Required(ErrorMessage = "Required")]
            [EmailAddress(ErrorMessage = "Wrong format")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Required")]
            [StringLength(100, ErrorMessage = "{0} mora biti duga između {2} i {1} znaka.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Display(Name = "Slažem se sa uvjetima i odredbama")]
            public bool AgreeToTerms { get; set; }
        }
        

        public void OnGet(string returnUrl = null) => ReturnUrl = returnUrl;

        public IActionResult OnPostAsync(string returnUrl = null)
        {
            return Page();
        }

       
    }
}
