using Microsoft.AspNetCore.Identity;

namespace Vacations.Core.Models.Identity
{
    public class EmployeeRole : IdentityUserRole<int>
    {
        public virtual Employee Employee { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}
