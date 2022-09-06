using System.ComponentModel.DataAnnotations;

namespace Vacations.Models.ViewModels.Domain;

public class CreateCompanyViewModel
{
    [Required(ErrorMessage = "Required")]
    [Display(Name = "Company name")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Required")]
    [Display(Name = "Company description")]
    public string Description { get; set; }
}