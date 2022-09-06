using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Vacations.Core;
using Vacations.Core.Models.Domain;
using Vacations.Models.DTOs;

namespace Vacations.Models.ViewModels.Domain;

public class IndexDepartmentViewModel
{
    public List<DepartmentDto> Departments { get; set; }

    public IndexDepartmentViewModel()
    {
        Departments = new List<DepartmentDto>();
    }
    
    public async Task PrepareData(IUnitOfWork unitOfWork, IMapper mapper)
    {
        var departments = await unitOfWork.DomainRepository.GetDepartmentsForIndex();
        Departments = mapper.Map<List<Department>, List<DepartmentDto>>(departments);
    }
}