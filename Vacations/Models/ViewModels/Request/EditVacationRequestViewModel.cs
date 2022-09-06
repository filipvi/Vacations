using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vacations.Core;
using Vacations.Models.Exceptions;

namespace Vacations.Models.ViewModels.Request;

public class EditVacationRequestViewModel
{
    #region Properties

    public int Id { get; set; }
    public int EmployeeId { get; set; }

    [Required(ErrorMessage = "Required")]
    [Display(Name = "Start date")]
    public string StartDate { get; set; }
    
    [Required(ErrorMessage = "Required")]
    [Display(Name = "End date")]
    public string EndDate { get; set; }
    
    [Required(ErrorMessage = "Required")]
    [Display(Name = "Description")]
    public string Description { get; set; }
    
    [Required(ErrorMessage = "Required")]
    [Display(Name = "Replacement employees (select one or more)")]
    public int[] ReplacementEmployeeIds { get; set; }

    public List<SelectListItem> ReplacementEmployeesSelectList { get; set; }
    
    // Helpers
    public int RemainingNumberOfVacationCurrentYear { get; set; }

    #endregion Properties

    public EditVacationRequestViewModel()
    {
        ReplacementEmployeesSelectList = new List<SelectListItem>();
    }

    public async Task PrepareData(IUnitOfWork unitOfWork, IMapper mapper)
    {
        var vacationRequest = await unitOfWork.RequestRepository.GetVacationRequestByIdAsync(Id);
        
        if (EmployeeId != vacationRequest.EmployeeId)
        {
            throw new UserNotAllowedException("You can edit only your requests!");
        }

        mapper.Map(vacationRequest, this);
        
        RemainingNumberOfVacationCurrentYear = await unitOfWork.RequestRepository
            .GetRemainingDaysOfVacationCurrentYearAsync(EmployeeId);
        ReplacementEmployeeIds = vacationRequest.VacationRequestReplacementEmployees.Select(x => x.EmployeeId).ToArray();
    }

    public async Task PrepareSelectLists(IUnitOfWork unitOfWork)
    {
        ReplacementEmployeesSelectList = await unitOfWork.RequestRepository
            .PrepareReplacementEmployeesSelectListAsync(EmployeeId);
    }

}