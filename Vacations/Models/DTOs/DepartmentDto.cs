namespace Vacations.Models.DTOs;

public class DepartmentDto
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int NumberOfEmployees { get; set; }
}