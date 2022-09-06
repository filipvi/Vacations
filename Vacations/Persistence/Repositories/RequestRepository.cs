using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vacations.Core.Interfaces;
using Vacations.Core.Models.Vacation;
using Vacations.Models.Enums;
using Vacations.Models.Exceptions;
using Vacations.Models.ViewModels.Request;
using Vacations.Utilities.Extensions;
using Vacations.Utilities.Helpers;

namespace Vacations.Persistence.Repositories;

public class RequestRepository : IRequestRepository
{
    private readonly ApplicationDbContext _context;

    public RequestRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetRemainingDaysOfVacationCurrentYearAsync(int employeeId)
    {
        int currentYear = DateTime.Now.Year;

        var employee = await _context.Employees
            .Include(x => x.VacationRequests)
            .SingleOrDefaultAsync(x => x.Id == employeeId);

        int usedDaysCurrentYear = 0;
        foreach (var vacationRequest in employee.VacationRequests
                     .Where(x => x.YearInfo == currentYear &&
                                 !x.IsDeleted &&
                                 (x.VacationRequestStatusId == (int)VacationRequestStatusEnums.Pending || x.VacationRequestStatusId == (int)VacationRequestStatusEnums.Approved)))
        {
            usedDaysCurrentYear += vacationRequest.DurationWorkingDays;
        }

        return employee.NumberOfVacationDaysPerYear.Value - usedDaysCurrentYear;
    }

    public async  Task<int> GetUsedDaysOfVacationCurrentYearAsync(int employeeId)
    {
        int currentYear = DateTime.Now.Year;

        var employee = await _context.Employees
            .Include(x => x.VacationRequests)
            .SingleOrDefaultAsync(x => x.Id == employeeId);

        int usedDaysCurrentYear = 0;
        foreach (var vacationRequest in employee.VacationRequests
                     .Where(x => x.YearInfo == currentYear &&
                                 !x.IsDeleted &&
                                 (x.VacationRequestStatusId == (int)VacationRequestStatusEnums.Pending || x.VacationRequestStatusId == (int)VacationRequestStatusEnums.Approved)))
        {
            usedDaysCurrentYear += vacationRequest.DurationWorkingDays;
        }

        return usedDaysCurrentYear;    
    }

    public async Task<int> GetAvailableDaysForEmployeeAsync(int employeeId)
    {
        var employee = await _context.Employees.SingleOrDefaultAsync(x => x.Id == employeeId);
        return employee.NumberOfVacationDaysPerYear.Value;
    }

    public async Task<List<SelectListItem>> PrepareReplacementEmployeesSelectListAsync(int employeeId)
    {
        var currentEmployee = await _context.Employees
            .Include(x => x.Department)
            .SingleOrDefaultAsync(x => x.Id == employeeId);

        var employeesForReplacement = await _context.Employees
            .Where(x => x.DepartmentId == currentEmployee.DepartmentId && x.Id != employeeId)
            .ToListAsync();

        return employeesForReplacement.ToSelectList(x => x.Id, x => x.FirstName + " " + x.LastName);
    }

    public async Task<List<SelectListItem>> PrepareVacationYearSelectListAsync(int remainingNumberOfVacationCurrentYear,
        int remainingNumberOfVacationPastYear,
        int employeeId)
    {
        List<SelectListItem> vacationYearSelectList = new List<SelectListItem>();

        var currentYear = DateTime.Now.Year;
        var employee = await _context.Employees.SingleOrDefaultAsync(x => x.Id == employeeId);
        int allowedNumberOfVacationDaysPerYear = employee.NumberOfVacationDaysPerYear.Value;

        //1. check for current year
        if (remainingNumberOfVacationCurrentYear > 0)
        {
            var requestsForCurrentYear = await _context.VacationRequests
                .Where(x => x.EmployeeId == employeeId &&
                            !x.IsDeleted &&
                            x.VacationRequestStatusId == (int) VacationRequestStatusEnums.Approved &&
                            x.StartDate > new DateTime(currentYear, 01, 01) &&
                            x.EndDate <= new DateTime(currentYear + 1, 07, 01))
                .ToListAsync();

            if (requestsForCurrentYear.Count < allowedNumberOfVacationDaysPerYear)
            {
                vacationYearSelectList.Add(new SelectListItem
                {
                    Text = currentYear.ToString(),
                    Value = currentYear.ToString()
                });
            }
        }

        //2. check for past year
        if (remainingNumberOfVacationPastYear > 0)
        {
            var requestsForNextYear = await _context.VacationRequests
                .Where(x => x.EmployeeId == employeeId &&
                            !x.IsDeleted &&
                            x.VacationRequestStatusId == (int) VacationRequestStatusEnums.Approved &&
                            x.StartDate > new DateTime(currentYear + 1, 01, 01) &&
                            x.EndDate <= new DateTime(currentYear + 2, 07, 01))
                .ToListAsync();

            if (requestsForNextYear.Count < allowedNumberOfVacationDaysPerYear)
            {
                vacationYearSelectList.Add(new SelectListItem
                {
                    Text = (currentYear + 1).ToString(),
                    Value = (currentYear + 1).ToString()
                });
            }
        }


        return vacationYearSelectList;
    }

    public async Task CreateVacationRequestAsync(VacationRequest vacationRequest)
    {
        await _context.VacationRequests.AddAsync(vacationRequest);
    }

    public async Task<List<SelectListItem>> GetVacationRequestStatusSelectListAsync()
    {
        var statuses = await _context.VacationRequestStatuses
            .Where(x => !x.IsDeleted)
            .ToListAsync();

        return statuses.ToSelectList(x => x.Id, x => x.Name);
    }

    public async Task<List<SelectListItem>> GetVacationRequestYearsSelectListAsync(int employeeId)
    {
        List<SelectListItem> yearsSelectList = new List<SelectListItem>();
        
        var years = await _context.VacationRequests
            .Include(x => x.Employee)
            .Select(x => x.YearInfo)
            .Distinct()
            .ToListAsync();

        foreach (var year in years)
        {
            yearsSelectList.Add(new SelectListItem
            {
                Value = year.ToString(),
                Text = year.ToString()
            });            
        }

        return yearsSelectList;
    }

    public IQueryable<VacationRequest> GetPendingVacationRequestsForIndexQueryable()
    {
        return _context.VacationRequests
            .Where(x => !x.IsDeleted);
    }

    public async Task<int> GetPendingVacationRequestsForIndexCountAsync()
    {
        var vacationRequests = _context.VacationRequests.Where(x => !x.IsDeleted);
        return await vacationRequests.CountAsync();
    }

    public async Task<VacationRequest> GetVacationRequestByIdAsync(int id)
    {
        return await _context.VacationRequests
            .Include(x => x.VacationRequestStatus)
            .Include(x => x.Employee)
            .Include(x => x.VacationRequestResponses).ThenInclude(y => y.Employee)
            .Include(x => x.VacationRequestReplacementEmployees).ThenInclude(y => y.Employee).ThenInclude(z => z.Department)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task EditVacationRequestAsync(EditVacationRequestViewModel viewModel)
    {
        var vacationRequest = await GetVacationRequestByIdAsync(viewModel.Id);

        if (vacationRequest.EmployeeId != viewModel.EmployeeId)
        {
            throw new UserNotAllowedException("You can edit only your requests!");
        }

        vacationRequest.Description = viewModel.Description;
        vacationRequest.StartDate = viewModel.StartDate.StringToDateTime();
        vacationRequest.EndDate = viewModel.EndDate.StringToDateTime();
        vacationRequest.YearInfo = vacationRequest.StartDate.Year;
        vacationRequest.DurationInDays = DateHelper.PrepareDurationInDays(vacationRequest.StartDate, vacationRequest.EndDate);
        vacationRequest.DurationWorkingDays = await DateHelper.PrepareDurationWorkingDays(vacationRequest.StartDate, vacationRequest.EndDate);
        
        // Update user contracts
        var existingReplacementEmployeeIds = vacationRequest.VacationRequestReplacementEmployees.Where(x => !x.IsDeleted).Select(x => x.EmployeeId).ToList();
        var viewModelReplacementEmployeeIds = viewModel.ReplacementEmployeeIds.ToList();

        if (viewModelReplacementEmployeeIds != null)
        {
            var newIds = viewModelReplacementEmployeeIds.Except(existingReplacementEmployeeIds);

            if (vacationRequest.VacationRequestReplacementEmployees == null)
            {
                vacationRequest.VacationRequestReplacementEmployees = new List<VacationRequestReplacementEmployees>();
            }

            foreach (var item in newIds)
            {
                vacationRequest.VacationRequestReplacementEmployees.Add(new VacationRequestReplacementEmployees
                {
                    EmployeeId = item
                });
            }
        }

        if (viewModelReplacementEmployeeIds != null)
        {
            var forDelete = existingReplacementEmployeeIds.Except(viewModelReplacementEmployeeIds);

            foreach (var item in forDelete)
            {
                vacationRequest.VacationRequestReplacementEmployees.FirstOrDefault(x => x.EmployeeId == item)!.IsDeleted = true;
            }
        }
    }

    public async Task DeleteRequestAsync(int id, int employeeId)
    {
        var vacationRequest = await GetVacationRequestByIdAsync(id);
        
        if (vacationRequest.EmployeeId != employeeId)
        {
            throw new UserNotAllowedException("You can delete only your requests!");
        }

        if (vacationRequest.VacationRequestStatusId != (int) VacationRequestStatusEnums.Pending)
        {
            throw new DeleteNotAllowedException("You can delete only pending requests!");
        }

        vacationRequest.IsDeleted = true;
    }

    public IQueryable<VacationRequest> GetAdminVacationRequestsForIndexQueryable(int statusId)
    {
        return _context.VacationRequests
            .Include(x => x.Employee).ThenInclude(y => y.Department).ThenInclude(z => z.Company)
            .Where(x => x.VacationRequestStatusId == statusId && !x.IsDeleted);
    }

    public async Task<int> GetAdminVacationRequestsForIndexCountAsync(int statusId)
    {
        return await _context.VacationRequests.Where(x => x.VacationRequestStatusId == statusId && !x.IsDeleted).CountAsync();
    }

    public async Task ChangeStatusAsync(ChangeStatusViewModel viewModel, int userId)
    {
        var request = await _context.VacationRequests
            .Include(x => x.VacationRequestResponses)
            .SingleOrDefaultAsync(x => x.Id == viewModel.Id);

        request.VacationRequestStatusId = viewModel.StatusId;

        if (request.VacationRequestResponses == null || request.VacationRequestResponses.Count == 0)
        {
            request.VacationRequestResponses = new List<VacationRequestResponse>();
            request.VacationRequestResponses.Add(new VacationRequestResponse
            {
                Description = viewModel.Description,
                EmployeeCreatedId = userId,
                VacationRequestStatusId = viewModel.StatusId,
                DateCreated = DateTime.Now
            });
        }
        else
        {
            var lastResponse = request.VacationRequestResponses.MaxBy(x => x.Id);
            lastResponse.IsDeleted = true;
            
            request.VacationRequestResponses.Add(new VacationRequestResponse
            {
                Description = viewModel.Description,
                EmployeeCreatedId = userId,
                VacationRequestStatusId = viewModel.StatusId,
                DateCreated = DateTime.Now
            });
        }
    }
}