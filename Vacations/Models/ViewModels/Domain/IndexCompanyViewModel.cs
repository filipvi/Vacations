using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Vacations.Core;
using Vacations.Core.Models.Domain;
using Vacations.Models.DTOs;

namespace Vacations.Models.ViewModels.Domain;

public class IndexCompanyViewModel
{
    public List<CompanyDto> Companies { get; set; }

    public IndexCompanyViewModel()
    {
        Companies = new List<CompanyDto>();
    }
    
    public async Task PrepareData(IUnitOfWork unitOfWork, IMapper mapper)
    {
        var companies = await unitOfWork.DomainRepository.GetCompaniesForIndex();
        Companies = mapper.Map<List<Company>, List<CompanyDto>>(companies);
    }
}