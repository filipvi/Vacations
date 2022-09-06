using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Vacations.Core;
using Vacations.Models.DTOs;

namespace Vacations.Models.ViewModels.Domain;

public class DetailsDepartmentViewModel
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    
    [Display(Name = "Company name")]
    public string CompanyName { get; set; }
    
    [Display(Name = "Department name")]
    public string Name { get; set; }
    
    [Display(Name = "Department descritpion")]
    public string Description { get; set; }
    public List<EmployeeDto>  Employees { get; set; }

    public DetailsDepartmentViewModel()
    {
        Employees = new List<EmployeeDto>();
    }

    public async Task PrepareData(IUnitOfWork unitOfWork, IMapper mapper)
    {
        var department = await unitOfWork.DomainRepository.GetDepartmentForDetailsAsync(Id);
        mapper.Map(department, this);
    }
}