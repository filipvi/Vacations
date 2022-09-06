using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Vacations.Core;
using Vacations.Core.Models.Identity;
using Vacations.Models.DTOs;

namespace Vacations.Models.ViewModels.Domain;

public class IndexEmployeeViewModel
{
    public List<EmployeeDto> Employees { get; set; }

    public IndexEmployeeViewModel()
    {
        Employees = new List<EmployeeDto>();
    }
    
    public async Task PrepareData(IUnitOfWork unitOfWork, IMapper mapper)
    {
        var employees = await unitOfWork.DomainRepository.GetEmployeesForIndex();
        Employees = mapper.Map<List<Employee>, List<EmployeeDto>>(employees);
    }
}