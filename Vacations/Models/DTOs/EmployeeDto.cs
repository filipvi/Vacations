namespace Vacations.Models.DTOs;

public class EmployeeDto
{
    public int Id { get; set; }
    public int DepartmentId { get; set; }
    public string Department { get; set; }
    public string Company { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => FirstName + " " + LastName;
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int? NumberOfVacationDaysPerYear { get; set; }
    
}