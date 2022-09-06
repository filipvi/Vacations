using System.Collections.Generic;
using Vacations.Core.Models.Identity;

namespace Vacations.Core.Models.Domain
{
    public class Department
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        public virtual List<Employee> Employees { get; set; }
        public virtual Company Company { get; set; }
    }
}
