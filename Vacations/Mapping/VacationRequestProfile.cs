using AutoMapper;
using Vacations.Core.Models.Vacation;
using Vacations.Models.DTOs;
using Vacations.Models.ViewModels.Request;
using Vacations.Utilities.Extensions;

namespace Vacations.Mapping;

public class VacationRequestProfile : Profile
{
    public VacationRequestProfile()
    {
        #region VacationRequest -> VacationRequestDto

        CreateMap<VacationRequest, VacationRequestDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => s.EmployeeId))
            .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee.FirstName + " " + s.Employee.LastName))
            .ForMember(d => d.StatusId, opt => opt.MapFrom(s => s.VacationRequestStatusId))
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.VacationRequestStatus.Name))
            .ForMember(d => d.DateCreated, opt => opt.MapFrom(s => s.DateCreated.DateToString()))
            .ForMember(d => d.DateFrom, opt => opt.MapFrom(s => s.StartDate.DateToString()))
            .ForMember(d => d.DateTo, opt => opt.MapFrom(s => s.EndDate.DateToString()))
            .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
            .ForMember(d => d.Year, opt => opt.MapFrom(s => s.YearInfo))
            .ForMember(d => d.DurationInWorkingDays, opt => opt.MapFrom(s => s.DurationWorkingDays))
            .ForMember(d => d.DurationInDays, opt => opt.MapFrom(s => s.DurationInDays))
            .ForMember(d => d.DepartmentId, opt => opt.MapFrom(s => s.Employee.DepartmentId))
            .ForMember(d => d.Department, opt => opt.MapFrom(s => s.Employee.Department.Name))
            .ForMember(d => d.CompanyId, opt => opt.MapFrom(s => s.Employee.Department.CompanyId))
            .ForMember(d => d.Company, opt => opt.MapFrom(s => s.Employee.Department.Company.Name))
            .ForAllOtherMembers(opt => opt.Ignore());

        #endregion VacationRequest -> VacationRequestDto
        
        #region VacationRequest -> DetailsVacationRequestViewModel

        CreateMap<VacationRequest, DetailsVacationRequestViewModel>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => s.EmployeeId))
            .ForMember(d => d.VacationRequestStatusId, opt => opt.MapFrom(s => s.VacationRequestStatusId))
            .ForMember(d => d.VacationRequestStatus, opt => opt.MapFrom(s => s.VacationRequestStatus.Name))
            .ForMember(d => d.IsDeleted, opt => opt.MapFrom(s => s.IsDeleted))
            .ForMember(d => d.StartDate, opt => opt.MapFrom(s => s.StartDate.DateToString()))
            .ForMember(d => d.EndDate, opt => opt.MapFrom(s => s.EndDate.DateToString()))
            .ForMember(d => d.DateCreated, opt => opt.MapFrom(s => s.DateCreated.DateToString()))
            .ForMember(d => d.YearInfo, opt => opt.MapFrom(s => s.YearInfo))
            .ForMember(d => d.DurationInWorkingDays, opt => opt.MapFrom(s => s.DurationWorkingDays))
            .ForMember(d => d.DurationInDays, opt => opt.MapFrom(s => s.DurationInDays))
            .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
            .ForAllOtherMembers(opt => opt.Ignore());

        CreateMap<VacationRequestReplacementEmployees, DetailsVacationRequestReplacementEmployeesViewModel>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => s.EmployeeId))
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.Employee.FirstName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.Employee.LastName))
            .ForMember(d => d.Department, opt => opt.MapFrom(s => s.Employee.Department.Name));
        
        
        #endregion VacationRequest -> DetailsVacationRequestViewModel
        
        #region VacationRequest -> DetailsVacationRequestAdminViewModel

        CreateMap<VacationRequest, DetailsVacationRequestAdminViewModel>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => s.EmployeeId))
            .ForMember(d => d.VacationRequestStatusId, opt => opt.MapFrom(s => s.VacationRequestStatusId))
            .ForMember(d => d.VacationRequestStatus, opt => opt.MapFrom(s => s.VacationRequestStatus.Name))
            .ForMember(d => d.IsDeleted, opt => opt.MapFrom(s => s.IsDeleted))
            .ForMember(d => d.StartDate, opt => opt.MapFrom(s => s.StartDate.DateToString()))
            .ForMember(d => d.EndDate, opt => opt.MapFrom(s => s.EndDate.DateToString()))
            .ForMember(d => d.DateCreated, opt => opt.MapFrom(s => s.DateCreated.DateToString()))
            .ForMember(d => d.YearInfo, opt => opt.MapFrom(s => s.YearInfo))
            .ForMember(d => d.DurationInWorkingDays, opt => opt.MapFrom(s => s.DurationWorkingDays))
            .ForMember(d => d.DurationInDays, opt => opt.MapFrom(s => s.DurationInDays))
            .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
            .ForAllOtherMembers(opt => opt.Ignore());

        CreateMap<VacationRequestReplacementEmployees, DetailsVacationRequestReplacementEmployeesViewModel>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => s.EmployeeId))
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.Employee.FirstName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.Employee.LastName))
            .ForMember(d => d.Department, opt => opt.MapFrom(s => s.Employee.Department.Name));
        
            
            
        #endregion VacationRequest -> DetailsVacationRequestAdminViewModel
        
        
        #region VacationRequest -> EditVacationRequestViewModel

        CreateMap<VacationRequest, EditVacationRequestViewModel>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => s.EmployeeId))
            .ForMember(d => d.StartDate, opt => opt.MapFrom(s => s.StartDate.DateToString()))
            .ForMember(d => d.EndDate, opt => opt.MapFrom(s => s.EndDate.DateToString()))
            .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
            .ForAllOtherMembers(opt => opt.Ignore());

        #endregion VacationRequest -> EditVacationRequestViewModel
        
        #region VacationRequestResponse -> DetailsVacationRequestResponseViewModel

        CreateMap<VacationRequestResponse, DetailsVacationRequestResponseViewModel>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee.FirstName + " " +s.Employee.LastName))
            .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
            .ForMember(d => d.DateCreated, opt => opt.MapFrom(s => s.DateCreated.DateToString()))
            .ForMember(d => d.StatusId, opt => opt.MapFrom(s => s.VacationRequestStatusId))
            .ForAllOtherMembers(opt => opt.Ignore());

        #endregion VacationRequestResponse -> DetailsVacationRequestResponseViewModel
    }
}