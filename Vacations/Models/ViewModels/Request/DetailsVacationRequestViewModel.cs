using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Vacations.Core;
using Vacations.Models.Exceptions;

namespace Vacations.Models.ViewModels.Request;

public class DetailsVacationRequestViewModel
{
    #region Properties

    public int Id { get; set; }

    public int EmployeeId { get; set; }
    
    [Display(Name = "Request status")]
    public string VacationRequestStatus { get; set; }
    public int VacationRequestStatusId { get; set; }


    [Display(Name = "Start date")]
    public string StartDate { get; set; }
    
    [Display(Name = "End date")]
    public string EndDate { get; set; }
    
    [Display(Name = "Date created")]
    public string DateCreated { get; set; }
    
    [Display(Name = "Year")]
    public string YearInfo { get; set; }

    [Display(Name = "Duration in days")]
    public string DurationInDays { get; set; }
    
    [Display(Name = "Duration in working days")]
    public string DurationInWorkingDays { get; set; }
    
    [Display(Name = "Description")]
    public string Description { get; set; }
    public bool IsDeleted { get; set; }
    
    public List<DetailsVacationRequestReplacementEmployeesViewModel> ReplacementEmployees { get; set; }

    public List<DetailsVacationRequestResponseViewModel> Responses { get; set; }

    #endregion Properties

    public DetailsVacationRequestViewModel()
    {
        ReplacementEmployees = new List<DetailsVacationRequestReplacementEmployeesViewModel>();
        Responses = new List<DetailsVacationRequestResponseViewModel>();
    }

    public async Task PrepareData(IUnitOfWork unitOfWork, IMapper mapper)
    {
        var vacationRequest = await unitOfWork.RequestRepository.GetVacationRequestByIdAsync(Id);

        if (vacationRequest.IsDeleted)
        {
            throw new RecordNotFoundException("Vacation request not found!");
        }
        
        if (EmployeeId != vacationRequest.EmployeeId)
        {
            throw new UserNotAllowedException("You can view only your requests!");
        }
        
        mapper.Map(vacationRequest, this);
        mapper.Map(vacationRequest.VacationRequestReplacementEmployees.Where(x => !x.IsDeleted), ReplacementEmployees);
        mapper.Map(vacationRequest.VacationRequestResponses, Responses);
    }
}