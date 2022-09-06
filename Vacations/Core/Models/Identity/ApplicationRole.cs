using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Vacations.Core.Models.Identity
{
    public class ApplicationRole : IdentityRole<int>
    {
        public virtual ICollection<EmployeeRole> EmployeeRoles { get; set; }
    }
}
