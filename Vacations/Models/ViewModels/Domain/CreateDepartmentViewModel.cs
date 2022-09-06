using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Vacations.Core;

namespace Vacations.Models.ViewModels.Domain;

public class CreateDepartmentViewModel
{
    [Required(ErrorMessage = "Required")]
    public int CompanyId { get; set; }

    public string CompanyName { get; set; }
    
    [Required(ErrorMessage = "Required")]
    [Display(Name = "Department name")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Required")]
    [Display(Name = "Department description")]
    public string Description { get; set; }

    public async Task PrepareData(IUnitOfWork unitOfWork)
    {
        var company = await unitOfWork.DomainRepository.GetCompanyForDetailsAsync(CompanyId);
        CompanyName = company.Name;
    }
}