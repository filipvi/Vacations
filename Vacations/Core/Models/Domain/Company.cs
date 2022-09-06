using System.Collections.Generic;

namespace Vacations.Core.Models.Domain
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<Department> Departments { get; set; }
    }
}
