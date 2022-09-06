using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Vacations.Core;

namespace Vacations.Models.ViewModels.Domain;

public class EditCompanyViewModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Required")]
    [Display(Name = "Company name")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Required")]
    [Display(Name = "Company description")]
    public string Description { get; set; }

    public async Task PrepareData(IUnitOfWork unitOfWork)
    {
        var company = await unitOfWork.DomainRepository.GetCompanyForDetailsAsync(Id);
        Name = company.Name;
        Description = company.Description;
    }

}