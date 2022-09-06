using System.Linq;
using AutoMapper;
using Vacations.Core.Models.Domain;
using Vacations.Core.Models.Identity;
using Vacations.Models.DTOs;
using Vacations.Models.ViewModels.Domain;

namespace Vacations.Mapping;

public class DomainProfile : Profile
{
    public DomainProfile()
    {
        #region Company -> CompanyDto

        CreateMap<Company, CompanyDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
            .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
            .ForMember(d => d.NumberOfDepartments, opt => opt.MapFrom(s => s.Departments.Count(x => !x.IsDeleted)))
            .ForAllOtherMembers(opt => opt.Ignore());

        #endregion Company -> CompanyDto
        
        #region Company -> CompanyDetailsViewModel

        CreateMap<Company, DetailsCompanyViewModel>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
            .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
            .ForMember(d => d.Departments, opt => opt.MapFrom(s => s.Departments.Where(x => !x.IsDeleted)))
            .ForAllOtherMembers(opt => opt.Ignore());

        CreateMap<Department, DepartmentDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.CompanyId, opt => opt.MapFrom(s => s.CompanyId))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
            .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
            .ForMember(d => d.NumberOfEmployees, opt => opt.MapFrom(s => s.Employees.Count()));

        #endregion Company -> CompanyDetailsViewModel
        
        #region Department -> DepartmentDetailsViewModel

        CreateMap<Department, DetailsDepartmentViewModel>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.CompanyId, opt => opt.MapFrom(s => s.CompanyId))
            .ForMember(d => d.CompanyName, opt => opt.MapFrom(s => s.Company.Name))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
            .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
            .ForMember(d => d.Employees, opt => opt.MapFrom(s => s.Employees))
            .ForAllOtherMembers(opt => opt.Ignore());

        CreateMap<Employee, EmployeeDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.DepartmentId, opt => opt.MapFrom(s => s.DepartmentId))
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber))
            .ForMember(d => d.NumberOfVacationDaysPerYear, opt => opt.MapFrom(s => s.NumberOfVacationDaysPerYear));

        #endregion Department -> DepartmentDetailsViewModel
        
        #region Department -> DepartmentDto

        CreateMap<Department, DepartmentDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
            .ForMember(d => d.CompanyName, opt => opt.MapFrom(s => s.Company.Name))
            .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
            .ForMember(d => d.NumberOfEmployees, opt => opt.MapFrom(s => s.Employees.Count()))
            .ForAllOtherMembers(opt => opt.Ignore());

        #endregion Department -> DepartmentDto
        
        #region Employee -> EmployeeDto

        CreateMap<Employee, EmployeeDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.DepartmentId, opt => opt.MapFrom(s => s.DepartmentId))
            .ForMember(d => d.Company, opt => opt.MapFrom(s => s.Department.Company.Name))
            .ForMember(d => d.Department, opt => opt.MapFrom(s => s.Department.Name))
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber))
            .ForMember(d => d.NumberOfVacationDaysPerYear, opt => opt.MapFrom(s => s.NumberOfVacationDaysPerYear))
            .ForAllOtherMembers(opt => opt.Ignore());

        #endregion Employee -> EmployeeDto
        
        #region Employee -> EmployeeDetailsViewModel
        
        CreateMap<Employee, DetailsEmployeeViewModel>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.DepartmentId, opt => opt.MapFrom(s => s.DepartmentId))
            .ForMember(d => d.Company, opt => opt.MapFrom(s => s.Department.Company.Name))
            .ForMember(d => d.Department, opt => opt.MapFrom(s => s.Department.Name))
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber))
            .ForMember(d => d.NumberOfVacationDaysPerYear, opt => opt.MapFrom(s => s.NumberOfVacationDaysPerYear))
            .ForAllOtherMembers(opt => opt.Ignore());

        CreateMap<Department, DepartmentDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.CompanyId, opt => opt.MapFrom(s => s.CompanyId))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
            .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
            .ForMember(d => d.NumberOfEmployees, opt => opt.MapFrom(s => s.Employees.Count()));

        #endregion Employee -> EmployeeDetailsViewModel
        
        
    }
    
}