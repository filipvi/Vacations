using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Vacations.Core.Models.Domain;
using Vacations.Core.Models.Vacation;

namespace Vacations.Core.Models.Identity
{
    public class Employee : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? NumberOfVacationDaysPerYear { get; set; }
        public int? DepartmentId { get; set; }

        public virtual ICollection<EmployeeRole> UserRoles { get; set; }
        public virtual ICollection<VacationRequest> VacationRequests { get; set; }
        public virtual Department Department { get; set; }
    }
}
