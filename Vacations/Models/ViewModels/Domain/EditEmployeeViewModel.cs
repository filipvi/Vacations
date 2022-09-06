using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vacations.Core;

namespace Vacations.Models.ViewModels.Domain;

public class EditEmployeeViewModel
{
    public int Id { get; set; }
    
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
    [Display(Name = "Number of vacation days per year")]
    public int NumberOfVacationDaysPerYear { get; set; }

    [Required(ErrorMessage = "Required")]
    [Display(Name = "Company")]
    public int CompanyId { get; set; }

    [Required(ErrorMessage = "Required")]
    [Display(Name = "Department")]
    public int DepartmentId { get; set; }
    
    [Display(Name = "Department name")]
    public string DepartmentName { get; set; }

    public List<SelectListItem> CompaniesSelectList { get; set; }
    public List<SelectListItem> DepartmentsSelectList { get; set; }

    public EditEmployeeViewModel()
    {
        DepartmentsSelectList = new List<SelectListItem>();
        CompaniesSelectList = new List<SelectListItem>();
    }
    
    public async Task PrepareData(IUnitOfWork unitOfWork)
    {
        var employee = await unitOfWork.DomainRepository.GetEmployeeForDetailsByIdAsync(Id);
        FirstName = employee.FirstName;
        LastName = employee.LastName;
        Email = employee.Email;
        NumberOfVacationDaysPerYear = employee.NumberOfVacationDaysPerYear.Value;
        CompanyId = employee.Department.CompanyId;
        DepartmentId = employee.DepartmentId.Value;
        DepartmentName = employee.Department.Name;
    }

    public async Task PrepareSelectLists(IUnitOfWork unitOfWork)
    {
        CompaniesSelectList = await unitOfWork.DomainRepository.GetCompaniesSelectListAsync();
        DepartmentsSelectList = await unitOfWork.DomainRepository.GetDepartmentsSelectListAsync(CompanyId);
    }

    public async Task PrepareDepartmentsSelectList(int companyId, IUnitOfWork unitOfWork)
    {
        DepartmentsSelectList = await unitOfWork.DomainRepository.GetDepartmentsSelectListAsync(companyId);
    }
}