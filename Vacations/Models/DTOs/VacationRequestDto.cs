namespace Vacations.Models.DTOs;

public class VacationRequestDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string Employee { get; set; }
    public int StatusId { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }
    public string DateCreated { get; set; }
    public string DateFrom { get; set; }
    public string DateTo { get; set; }
    public int Year { get; set; }
    public int DurationInWorkingDays { get; set; }
    public int DurationInDays { get; set; }
    public int CompanyId { get; set; }
    public int DepartmentId { get; set; }
    public string Company { get; set; }
    public string Department { get; set; }
}