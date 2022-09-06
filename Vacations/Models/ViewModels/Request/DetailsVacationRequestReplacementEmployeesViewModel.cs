namespace Vacations.Models.ViewModels.Request;

public class DetailsVacationRequestReplacementEmployeesViewModel
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => FirstName + " " + LastName;

    public string Department { get; set; }
}