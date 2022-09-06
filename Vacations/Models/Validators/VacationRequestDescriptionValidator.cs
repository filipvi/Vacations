using System.ComponentModel.DataAnnotations;
using Vacations.Models.Enums;
using Vacations.Models.ViewModels.Request;

namespace Vacations.Models.Validators;

public class VacationRequestDescriptionValidator : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (validationContext.ObjectInstance.GetType() == typeof(ChangeStatusViewModel))
        {
            var viewModel = (ChangeStatusViewModel)validationContext.ObjectInstance;

            if (viewModel.StatusId == (int)VacationRequestStatusEnums.Invalid || viewModel.StatusId == (int)VacationRequestStatusEnums.Rejected)
            {
                if (string.IsNullOrWhiteSpace(viewModel.Description))
                {
                    return new ValidationResult("Description is required for chosen status!");
                }
            }
        }

        return ValidationResult.Success;
    }
}
