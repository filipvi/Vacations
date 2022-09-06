using System;
using System.Collections.Generic;
using Vacations.Core.Models.Codex;
using Vacations.Core.Models.Identity;

namespace Vacations.Core.Models.Vacation
{
    public class VacationRequest
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int VacationRequestStatusId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int YearInfo { get; set; } // While saving request define which year  
        public string Description { get; set; }
        public int DurationInDays { get; set; }
        public int DurationWorkingDays { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual VacationRequestStatus VacationRequestStatus { get; set; }
        public virtual List<VacationRequestResponse> VacationRequestResponses { get; set; }
        public virtual List<VacationRequestReplacementEmployees> VacationRequestReplacementEmployees { get; set; }

    }
}
