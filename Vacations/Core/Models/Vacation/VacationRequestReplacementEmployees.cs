using Vacations.Core.Models.Identity;

namespace Vacations.Core.Models.Vacation;

public class VacationRequestReplacementEmployees
{
    public int Id { get; set; }

    public int VacationRequestId { get; set; }

    public int EmployeeId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual VacationRequest VacationRequest { get; set; }
    public virtual Employee Employee { get; set; }
}