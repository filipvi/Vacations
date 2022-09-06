using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Vacations.Core;
using Vacations.Models.DTOs;

namespace Vacations.Models.ViewModels.Domain;

public class DetailsCompanyViewModel
{
    public int Id { get; set; }
    
    [Display(Name = "Company name")]
    public string Name { get; set; }
    
    [Display(Name = "Company description")]
    public string Description { get; set; }
    public List<DepartmentDto>  Departments { get; set; }

    public DetailsCompanyViewModel()
    {
        Departments = new List<DepartmentDto>();
    }

    public async Task PrepareData(IUnitOfWork unitOfWork, IMapper mapper)
    {
        var company = await unitOfWork.DomainRepository.GetCompanyForDetailsAsync(Id);
        mapper.Map(company, this);
    }
}