
//using Vacations.Models.Security;
using Microsoft.AspNetCore.Mvc;
using Vacations.Utilities.Navigation;
using Vacations.Utilities.Security;

namespace Vacations.ViewComponents
{
    public class NavigationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var userAuthenticated = User.Identity != null && User.Identity.IsAuthenticated;

            // if user is not authenticated
            if (!userAuthenticated)
            {
                NavigationModel.NavigationJsonFile = "NavBar/navAnonymous.json";
            }
            else
            {
                // user is authenticated
                if (User.IsInRole(UserRoles.Administrator))
                {
                    NavigationModel.NavigationJsonFile = "NavBar/navAdmin.json";
                }
                else
                {
                    NavigationModel.NavigationJsonFile = "NavBar/navEmployee.json";
                }
            }

            var items = NavigationModel.Full;

            return View(items);
        }
    }
}
