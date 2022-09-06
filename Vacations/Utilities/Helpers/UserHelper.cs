using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Vacations.Core.Models.Identity;

namespace Vacations.Utilities.Helpers;

public class UserHelper
{
    private readonly UserManager<Employee> _userManager;

    public UserHelper(UserManager<Employee> userManager)
    {
        _userManager = userManager;
    }

    public static int GetUserIdByClaimsIdentity(ClaimsIdentity userIdentity)
    {
        var claim = userIdentity.FindFirst(ClaimTypes.NameIdentifier);
        var userIdString = claim.Value;

        return int.Parse(userIdString);
    }

    public string GetUserFirstAndLastName(ClaimsPrincipal currentUser)
    {
        var user = _userManager.GetUserAsync(currentUser);
        if (user == null)
        {
            return string.Empty;
        }

        return user.Result.FirstName + " " + user.Result.LastName;
    }
}