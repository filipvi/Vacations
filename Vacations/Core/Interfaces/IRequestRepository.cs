using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vacations.Core.Models.Vacation;
using Vacations.Models.ViewModels.Request;

namespace Vacations.Core.Interfaces;

public interface IRequestRepository
{
    Task<int> GetRemainingDaysOfVacationCurrentYearAsync(int employeeId);
    Task<int> GetUsedDaysOfVacationCurrentYearAsync(int employeeId);
    Task<int> GetAvailableDaysForEmployeeAsync(int employeeId);
    Task<List<SelectListItem>> PrepareReplacementEmployeesSelectListAsync(int employeeId);
    Task<List<SelectListItem>> PrepareVacationYearSelectListAsync(int remainingNumberOfVacationCurrentYear, int remainingNumberOfVacationPastYear, int employeeId);
    Task CreateVacationRequestAsync(VacationRequest vacationRequest);
    Task<List<SelectListItem>> GetVacationRequestStatusSelectListAsync();
    Task<List<SelectListItem>> GetVacationRequestYearsSelectListAsync(int employeeId);
    IQueryable<VacationRequest> GetPendingVacationRequestsForIndexQueryable();
    Task<int> GetPendingVacationRequestsForIndexCountAsync();
    Task<VacationRequest> GetVacationRequestByIdAsync(int id);
    Task EditVacationRequestAsync(EditVacationRequestViewModel viewModel);
    Task DeleteRequestAsync(int id, int employeeId);
    IQueryable<VacationRequest> GetAdminVacationRequestsForIndexQueryable(int statusId);
    Task<int> GetAdminVacationRequestsForIndexCountAsync(int statusId);
    Task ChangeStatusAsync(ChangeStatusViewModel viewModel, int getLoggedInUserId);
}