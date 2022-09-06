using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vacations.Core;
using Vacations.Core.Models.Vacation;
using Vacations.Models.Enums;
using Vacations.Models.Exceptions;
using Vacations.Utilities.Extensions;
using Vacations.Utilities.Helpers;

namespace Vacations.Models.ViewModels.Request;

public class CreateVacationRequestViewModel
{
    #region Properties

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

    // For saving purposes
    public VacationRequest VacationRequest { get; set; }
    
    #endregion Properties

    public CreateVacationRequestViewModel()
    {
        ReplacementEmployeesSelectList = new List<SelectListItem>();
    }
    
    public async Task PrepareData(int employeeId, IUnitOfWork unitOfWork, IMapper mapper)
    {
        var employee = await unitOfWork.DomainRepository.GetEmployeeByIdAsync(employeeId);
        EmployeeId = employee.Id;

        await CheckRemainingDays(employeeId, unitOfWork);

        if (RemainingNumberOfVacationCurrentYear == 0)
        {
            throw new ModelNotValidException("You don't have available days for vacation!");
        }
    }

    public async Task PrepareSelectLists(IUnitOfWork unitOfWork)
    {
        ReplacementEmployeesSelectList = await unitOfWork.RequestRepository
            .PrepareReplacementEmployeesSelectListAsync(EmployeeId);
    }

    private async Task CheckRemainingDays(int employeeId, IUnitOfWork unitOfWork)
    {
        RemainingNumberOfVacationCurrentYear = await unitOfWork.RequestRepository
            .GetRemainingDaysOfVacationCurrentYearAsync(employeeId);
    }

    public async Task PrepareDataForSaving(IUnitOfWork unitOfWork)
    {
        VacationRequest = new VacationRequest();

        VacationRequest.Description = Description;
        VacationRequest.DateCreated = DateTime.Now;
        VacationRequest.EmployeeId = EmployeeId;
        VacationRequest.StartDate = StartDate.StringToDateTime();
        VacationRequest.EndDate = EndDate.StringToDateTime();

        await CheckRemainingDays(EmployeeId, unitOfWork);
        if (RemainingNumberOfVacationCurrentYear == 0)
        {
            throw new ModelNotValidException("You don't have available days for vacation!");
        }
        
        VacationRequest.VacationRequestStatusId = (int) VacationRequestStatusEnums.Pending;
        VacationRequest.DurationInDays = DateHelper.PrepareDurationInDays(VacationRequest.StartDate, VacationRequest.EndDate);
        VacationRequest.DurationWorkingDays = await DateHelper.PrepareDurationWorkingDays(VacationRequest.StartDate, VacationRequest.EndDate);
        VacationRequest.YearInfo = VacationRequest.StartDate.Year;

        if (ReplacementEmployeeIds.Length > 0)
        {
            VacationRequest.VacationRequestReplacementEmployees = new List<VacationRequestReplacementEmployees>();

            foreach (var employeeId in ReplacementEmployeeIds)
            {
                VacationRequest.VacationRequestReplacementEmployees.Add(new VacationRequestReplacementEmployees
                {
                    EmployeeId = employeeId
                });
            }
        }
    }
}