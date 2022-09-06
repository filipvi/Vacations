#region Using

using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vacations.Models;

#endregion

namespace Vacations.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult LandPage()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("SignedIn", "Home");
            }
            return View();
        }

        //Redirected after login
        public IActionResult SignedIn()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult GlobalError()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult UserNotFound()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult NotAuthorizedError(string domainName)
        {
            return View("NotAuthorizedError", domainName);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}