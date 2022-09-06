using System;
using Vacations.Core.Models.Codex;
using Vacations.Core.Models.Identity;

namespace Vacations.Core.Models.Vacation
{
    public class VacationRequestResponse
    {
        public int Id { get; set; }
        public int VacationRequestId { get; set; }
        public int VacationRequestStatusId { get; set; }
        public string Description { get; set; }
        public int EmployeeCreatedId { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDeleted { get; set; }

        public virtual VacationRequest VacationRequest { get; set; }
        public virtual VacationRequestStatus VacationRequestStatus { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
