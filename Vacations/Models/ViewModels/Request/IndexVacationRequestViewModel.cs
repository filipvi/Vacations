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

public class IndexVacationRequestViewModel : VacationRequestDataTableProperties
{
    public int EmployeeId { get; set; }
    
    [Display(Name = "Request status")]
    public int VacationRequestStatusId { get; set; }

    [Display(Name = "Date created")]
    public string DateCreated { get; set; }
    
    [Display(Name = "Date from")]
    public string DateFrom { get; set; }
    
    [Display(Name = "Date to")]
    public string DateTo { get; set; }
    
    [Display(Name = "Vacation year")]
    public int YearId { get; set; }
    
    [Display(Name = "Duration in working days")]
    public int DurationInWorkingDays { get; set; }
    
    [Display(Name = "Duration in days")]
    public int DurationInDays { get; set; }

    public List<SelectListItem> VacationRequestStatusSelectList { get; set; }
    public List<SelectListItem> VacationRequestYearSelectList { get; set; }
    public List<VacationRequestDto> RequestList { get; set; }

    public IndexVacationRequestViewModel()
    {
        VacationRequestStatusSelectList = new List<SelectListItem>();
        VacationRequestYearSelectList = new List<SelectListItem>();
        RequestList = new List<VacationRequestDto>();
    }

    public async Task PrepareSelectLists(IUnitOfWork unitOfWork)
    {
        VacationRequestStatusSelectList = await unitOfWork.RequestRepository
            .GetVacationRequestStatusSelectListAsync();
        VacationRequestYearSelectList =
            await unitOfWork.RequestRepository.GetVacationRequestYearsSelectListAsync(EmployeeId);
    }

    public async Task GetData(IFormCollection form, IUnitOfWork unitOfWork, IMapper mapper)
    {
        ExtractDataTableProperties(form);

        IQueryable<VacationRequest> vacationRequests =
            unitOfWork.RequestRepository.GetPendingVacationRequestsForIndexQueryable();
        
        TotalRecords = await unitOfWork.RequestRepository
            .GetPendingVacationRequestsForIndexCountAsync();

        // apply search
        if (!string.IsNullOrWhiteSpace(SearchStatus) ||
            !string.IsNullOrWhiteSpace(SearchYear) ||
            !string.IsNullOrWhiteSpace(SearchDateCreated) ||
            !string.IsNullOrWhiteSpace(SearchDateFrom) ||
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
        SearchStatus = form["columns[0][search][value]"][0];
        SearchYear = form["columns[1][search][value]"][0];
        SearchDateFrom = form["columns[2][search][value]"][0];
        SearchDateTo = form["columns[3][search][value]"][0];

        Draw = form["draw"][0];
        Order = form["order[0][column]"][0];
        OrderDir = form["order[0][dir]"][0];
        StartRec = Convert.ToInt32(form["start"][0]);
        PageSize = Convert.ToInt32(form["length"][0]);
    }

    private IQueryable<VacationRequest> ApplySearch(IQueryable<VacationRequest> vacationRequest)
    {
        if (!string.IsNullOrWhiteSpace(SearchStatus))
        {
            var isStatus = int.TryParse(SearchStatus, out int statusId);

            if (isStatus)
            {
                vacationRequest = vacationRequest
                    .Where(x => x.VacationRequestStatusId == statusId);
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