using System;
using Microsoft.AspNetCore.Authorization;

namespace Vacations.Utilities.Security;

public class AuthorizeRoles : AuthorizeAttribute
{
    /// <summary>
    /// [AuthorizeRoles(RoleConstant1,RoleConstant2)]
    /// </summary>
    /// <param name="roles"></param>
    public AuthorizeRoles(params string[] roles)
    {
        Roles = String.Join(",", roles);
    }
}