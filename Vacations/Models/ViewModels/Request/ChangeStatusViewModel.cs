using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vacations.Core;
using Vacations.Models.Validators;

namespace Vacations.Models.ViewModels.Request;

public class ChangeStatusViewModel
{
    public int Id { get; set; }
    public int CurrentStatusId { get; set; }
    public string CurrentStatus { get; set; }

    [Required(ErrorMessage = "Required")]
    [Display(Name = "Vacation request status")]
    public int StatusId { get; set; }
    
    [VacationRequestDescriptionValidator]
    public string Description { get; set; }

    public List<SelectListItem> StatusSelectList { get; set; }

    public ChangeStatusViewModel()
    {
        StatusSelectList = new List<SelectListItem>();
    }
    
    public async Task PopulateDataAsync(IUnitOfWork unitOfWork)
    {
        var request = await unitOfWork.RequestRepository.GetVacationRequestByIdAsync(Id);
        CurrentStatusId = request.VacationRequestStatusId;
        CurrentStatus = request.VacationRequestStatus.Name;
    }

    public async Task PrepareSelectLists(IUnitOfWork unitOfWork)
    {
        StatusSelectList = await unitOfWork.RequestRepository.GetVacationRequestStatusSelectListAsync();
    }
}