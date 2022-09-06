using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vacations.Core;
using Vacations.Core.Models.Vacation;
using Vacations.Models.AuxiliaryModels;
using Vacations.Models.DTOs;

namespace Vacations.Models.ViewModels.Request;

public class IndexVacationRequestAdminViewModel : VacationRequestAdminDataTableProperties
{
    public int StatusId { get; set; }
    public string StatusInfo { get; set; }


    [Display(Name = "Employee")]
    public int EmployeeId { get; set; }
    
    [Display(Name = "Company")]
    public int CompanyId { get; set; }
    
    [Display(Name = "Department")]
    public int DepartmentId { get; set; }
    
    [Display(Name = "Date created")]
    public string DateCreated { get; set; }
    
    [Display(Name = "Date from")]
    public string DateFrom { get; set; }
    
    [Display(Name = "Date to")]
    public string DateTo { get; set; }
    
    [Display(Name = "Vacation year")]
    public int YearId { get; set; }
    
    public List<SelectListItem> EmployeesSelectList { get; set; }
    public List<SelectListItem> CompaniesSelectList { get; set; }
    public List<SelectListItem> DepartmentsSelectList { get; set; }
    public List<SelectListItem> VacationRequestYearSelectList { get; set; }
    public List<VacationRequestDto> RequestList { get; set; }

    public IndexVacationRequestAdminViewModel()
    {
        EmployeesSelectList = new List<SelectListItem>();
        CompaniesSelectList = new List<SelectListItem>();
        DepartmentsSelectList = new List<SelectListItem>();
        VacationRequestYearSelectList = new List<SelectListItem>();
        RequestList = new List<VacationRequestDto>();
    }

    public async Task PrepareSelectLists(IUnitOfWork unitOfWork)
    {
        EmployeesSelectList = await unitOfWork.DomainRepository.GetEmployeesSelectListAsync();
        CompaniesSelectList = await unitOfWork.DomainRepository.GetCompaniesSelectListAsync();
        DepartmentsSelectList = await unitOfWork.DomainRepository.GetDepartmentsSelectListAsync();
        VacationRequestYearSelectList =
            await unitOfWork.RequestRepository.GetVacationRequestYearsSelectListAsync(EmployeeId);
    }

    public async Task GetData(IFormCollection form, int statusId, IUnitOfWork unitOfWork, IMapper mapper)
    {
        ExtractDataTableProperties(form);

        IQueryable<VacationRequest> vacationRequests =
            unitOfWork.RequestRepository.GetAdminVacationRequestsForIndexQueryable(statusId);
        
        TotalRecords = await unitOfWork.RequestRepository
            .GetAdminVacationRequestsForIndexCountAsync(statusId);

        // apply search
        if (!string.IsNullOrWhiteSpace(SearchDateFrom) ||
            !string.IsNullOrWhiteSpace(SearchDateTo) ||
            !string.IsNullOrWhiteSpace(SearchYear) ||
            !string.IsNullOrWhiteSpace(SearchEmployee) ||
            !string.IsNullOrWhiteSpace(SearchCompany) ||
            !string.IsNullOrWhiteSpace(SearchDepartment) ||
            !string.IsNullOrWhiteSpace(SearchDateTo))
        {
            vacationRequests = ApplySearch(vacationRequests);
        }

        // sorting
        vacationRequests = SortByColumnWithOrder(vacationRequests);

        // count filtered data
        RecFilter = vacationRequests.Count();

        // pagination
        var requestsList = await vacationRequests.Skip(StartRec).Take(PageSize).ToListAsync();

        // map data
        RequestList = mapper.Map<List<VacationRequest>, List<VacationRequestDto>>(requestsList);
    }


    private void ExtractDataTableProperties(IFormCollection form)
    {
        SearchEmployee = form["columns[0][search][value]"][0];
        SearchCompany = form["columns[1][search][value]"][0];
        SearchDepartment = form["columns[2][search][value]"][0];
        SearchDateFrom = form["columns[4][search][value]"][0];
        SearchDateTo = form["columns[5][search][value]"][0];
        SearchYear = form["columns[6][search][value]"][0];
        Draw = form["draw"][0];
        Order = form["order[0][column]"][0];
        OrderDir = form["order[0][dir]"][0];
        StartRec = Convert.ToInt32(form["start"][0]);
        PageSize = Convert.ToInt32(form["length"][0]);
    }

    private IQueryable<VacationRequest> ApplySearch(IQueryable<VacationRequest> vacationRequest)
    {
        if (!string.IsNullOrWhiteSpace(SearchDateFrom))
        {
            string format = "dd.MM.yyyy";
            var isDate = DateTime.TryParseExact(SearchDateFrom, format, CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var searchDate);

            if (isDate)
            {
                vacationRequest = vacationRequest
                    .Where(x => x.StartDate == searchDate);
            }
        }
        
        if (!string.IsNullOrWhiteSpace(SearchDateTo))
        {
            string format = "dd.MM.yyyy";
            var isDate = DateTime.TryParseExact(SearchDateTo, format, CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var searchDate);

            if (isDate)
            {
                vacationRequest = vacationRequest
                    .Where(x => x.EndDate == searchDate);
            }
        }

        if (!string.IsNullOrWhiteSpace(SearchYear))
        {
            var isYear = int.TryParse(SearchYear, out int yearId);

            if (isYear)
            {
                vacationRequest = vacationRequest
                    .Where(x => x.YearInfo == yearId);   
            }
        }

        if (!string.IsNullOrWhiteSpace(SearchEmployee))
        {
            var isEmployee = int.TryParse(SearchEmployee, out int employeeId);

            if (isEmployee)
            {
                vacationRequest = vacationRequest
                    .Where(x => x.EmployeeId == employeeId);   
            }
        }

        if (!string.IsNullOrWhiteSpace(SearchCompany))
        {
            var isCompany = int.TryParse(SearchCompany, out int companyId);

            if (isCompany)
            {
                vacationRequest = vacationRequest
                    .Where(x => x.Employee.Department.CompanyId == companyId);   
            }
        }

        if (!string.IsNullOrWhiteSpace(SearchDepartment))
        {
            var isDepartment = int.TryParse(SearchDepartment, out int departmentId);

            if (isDepartment)
            {
                vacationRequest = vacationRequest
                    .Where(x => x.Employee.DepartmentId == departmentId);   
            }
        }

        return vacationRequest;
    }

    private IQueryable<VacationRequest> SortByColumnWithOrder(IQueryable<VacationRequest> vacationRequests)
    {
        //sorting
        switch (Order)
        {
            case "0":
                vacationRequests = OrderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase)
                    ? vacationRequests.OrderBy(p => p.StartDate)
                    : vacationRequests.OrderByDescending(p => p.StartDate);
                break;
            // case "1":
            //     vacationRequests = OrderDir.Equals("ASC", StringComparison.CurrentCultureIgnoreCase)
            //         ? vacationRequests.OrderBy(p => p.Contract.HuntingGround.Name)
            //         : vacationRequests.OrderByDescending(p => p.Contract.HuntingGround.Name);
            //     break;
            // case "2": // District
            //     // Setting.   
            //     vacationRequests = OrderDir.Equals("ASC", StringComparison.CurrentCultureIgnoreCase)
            //         ? vacationRequests.OrderBy(p => p.Contract.HuntingGround.District.Name)
            //         : vacationRequests.OrderByDescending(p => p.Contract.HuntingGround.District.Name);
            //     break;
            // case "3": // Hunting year
            //     // Setting.   
            //     vacationRequests = OrderDir.Equals("ASC", StringComparison.CurrentCultureIgnoreCase)
            //         ? vacationRequests.OrderBy(p => p.HuntingYearFrom)
            //         : vacationRequests.OrderByDescending(p => p.HuntingYearFrom);
            //     break;
            // case "4": // Partner
            //     // Setting.   
            //     vacationRequests = OrderDir.Equals("ASC", StringComparison.CurrentCultureIgnoreCase)
            //         ? vacationRequests.OrderBy(p => p.Partner.Name)
            //         : vacationRequests.OrderByDescending(p => p.Partner.Name);
            //     break;
            // case "5": // Release date
            //     // Setting.   
            //     vacationRequests = OrderDir.Equals("ASC", StringComparison.CurrentCultureIgnoreCase)
            //         ? vacationRequests.OrderBy(p => p.ReleaseDate)
            //         : vacationRequests.OrderByDescending(p => p.ReleaseDate);
            //     break;
            default: // Number desc
                // Setting.   
                vacationRequests = OrderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase)
                    ? vacationRequests.OrderByDescending(p => p.StartDate)
                    : vacationRequests.OrderBy(p => p.StartDate);
                break;
        }

        return vacationRequests;
    }
}