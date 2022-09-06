using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vacations.Core.Models.Domain;
using Vacations.Core.Models.Identity;
using Vacations.Models.ViewModels.Domain;

namespace Vacations.Core.Interfaces;

public interface IDomainRepository
{
    // Company
    Task<List<Company>> GetCompaniesForIndex();
    Task<Company> GetCompanyForDetailsAsync(int id);
    Task DeleteCompanyAsync(int id);
    Task CreateCompanyAsync(CreateCompanyViewModel viewModel);
    
    // Department
    Task CreateDepartmentAsync(CreateDepartmentViewModel viewModel);
    Task DeleteDepartmentAsync(int id);
    Task<Department> GetDepartmentForDetailsAsync(int id);
    Task<List<Department>> GetDepartmentsForIndex();
    
    // Employee
    Task CreateEmployeeAsync(CreateEmployeeViewModel viewModel);

    Task<List<Employee>> GetEmployeesForIndex();
    Task<Employee> GetEmployeeForDetailsByIdAsync(int id);
    Task DeleteEmployeeAsync(int id);
    Task<Employee> GetEmployeeByIdAsync(int employeeId);
    Task<List<SelectListItem>> GetEmployeesSelectListAsync();
    Task<List<SelectListItem>> GetCompaniesSelectListAsync();
    Task<List<SelectListItem>> GetDepartmentsSelectListAsync();
    Task<List<SelectListItem>> GetDepartmentsSelectListAsync(int companyId);

    Task EditCompanyAsync(EditCompanyViewModel viewModel);
    Task EditDepartmentAsync(EditDepartmentViewModel viewModel);
    Task EditEmployeeAsync(EditEmployeeViewModel viewModel);
}