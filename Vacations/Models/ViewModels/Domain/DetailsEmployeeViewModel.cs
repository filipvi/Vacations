using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Vacations.Core;

namespace Vacations.Models.ViewModels.Domain;

public class DetailsEmployeeViewModel
{
    public int Id { get; set; }
    public int DepartmentId { get; set; }
    
    [Display(Name = "Company name")]
    public string Company { get; set; }
    
    [Display(Name = "Department name")]
    public string Department { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    [Display(Name = "Name")]
    public string FullName => FirstName + " " + LastName;
    
    [Display(Name = "Email")]
    public string Email { get; set; }
    
    [Display(Name = "Phone number")]
    public string PhoneNumber { get; set; }
    
    [Display(Name = "Number of vacation days per year")]
    public int NumberOfVacationDaysPerYear { get; set; }

    public async Task  PrepareData(IUnitOfWork unitOfWork, IMapper mapper)
    {
        var employee = await unitOfWork.DomainRepository.GetEmployeeForDetailsByIdAsync(Id);
        mapper.Map(employee, this);
    }
}