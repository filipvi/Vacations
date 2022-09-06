using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Vacations.Core;

namespace Vacations.Models.ViewModels.Domain;

public class CreateEmployeeViewModel
{
    [Required(ErrorMessage = "Required")]
    [Display(Name = "First name")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Required")]
    [Display(Name = "Last name")]
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "Required")]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Required")]
    [Display(Name = "Password")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "Required")]
    [Display(Name = "Number of vacation days per year")]
    public int NumberOfVacationDaysPerYear { get; set; }
    
    [Required(ErrorMessage = "Required")]
    public int DepartmentId { get; set; }
    
    [Display(Name = "Department name")]
    public string DepartmentName { get; set; }

    public async Task PrepareData(IUnitOfWork unitOfWork)
    {
        var department = await unitOfWork.DomainRepository.GetDepartmentForDetailsAsync(DepartmentId);
        DepartmentName = department.Name;
    }
}