using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vacations.Core.Interfaces;
using Vacations.Core.Models.Domain;
using Vacations.Core.Models.Identity;
using Vacations.Models.Enums;
using Vacations.Models.Exceptions;
using Vacations.Models.ViewModels.Domain;
using Vacations.Utilities.Extensions;

namespace Vacations.Persistence.Repositories;

public class DomainRepository : IDomainRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher<Employee> _passwordHasher;
    private readonly UserManager<Employee> _userManager;


    public DomainRepository(ApplicationDbContext context, IPasswordHasher<Employee> passwordHasher,
        UserManager<Employee> userManager)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _userManager = userManager;
    }


    // Companies
    public async Task<List<Company>> GetCompaniesForIndex()
    {
        return await _context.Companies
            .Include(x => x.Departments).ThenInclude(y => y.Employees)
            .ToListAsync();
    }

    public async Task<Company> GetCompanyForDetailsAsync(int id)
    {
        return await _context.Companies
            .Include(x => x.Departments).ThenInclude(y => y.Employees)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task DeleteCompanyAsync(int id)
    {
        var company = await GetCompanyForDetailsAsync(id);

        if (company.Departments.Count(x => !x.IsDeleted) > 0)
            throw new DeleteNotAllowedException("Company has departments. Delete not allowed!");

        _context.Companies.Remove(company);
    }

    public async Task CreateCompanyAsync(CreateCompanyViewModel viewModel)
    {
        Company company = new Company();
        company.Name = viewModel.Name;
        company.Description = viewModel.Description;

        await _context.Companies.AddAsync(company);
    }

    // Departments
    public async Task CreateDepartmentAsync(CreateDepartmentViewModel viewModel)
    {
        Department department = new Department();
        department.CompanyId = viewModel.CompanyId;
        department.Name = viewModel.Name;
        department.Description = viewModel.Description;

        await _context.Departments.AddAsync(department);
    }

    public async Task<Department> GetDepartmentForDetailsAsync(int id)
    {
        return await _context.Departments
            .Include(x => x.Company)
            .Include(x => x.Employees)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Department>> GetDepartmentsForIndex()
    {
        return await _context.Departments
            .Include(x => x.Company)
            .Include(x => x.Employees)
            .ToListAsync();
    }

    public async Task DeleteDepartmentAsync(int id)
    {
        var department = await GetDepartmentForDetailsAsync(id);

        if (department.Employees.Any())
            throw new DeleteNotAllowedException("Department has employees. Delete not allowed!");

        _context.Departments.Remove(department);
    }


    // Employees
    public async Task CreateEmployeeAsync(CreateEmployeeViewModel viewModel)
    {
        Employee employee = new Employee();

        //  basic info
        employee.FirstName = viewModel.FirstName.TrimValue();
        employee.LastName = viewModel.LastName.TrimValue();
        employee.Email = viewModel.Email;
        employee.NumberOfVacationDaysPerYear = viewModel.NumberOfVacationDaysPerYear;
        employee.DepartmentId = viewModel.DepartmentId;
        employee.EmailConfirmed = true;
        employee.PhoneNumberConfirmed = true;

        employee.UserRoles = new List<EmployeeRole>();
        employee.UserRoles.Add(new EmployeeRole {RoleId = (int) RoleEnums.User});
        employee.NormalizedEmail = employee.Email.ToUpper();
        employee.UserName = employee.Email;
        employee.NormalizedUserName = employee.UserName.ToUpper();

        var hashedPassword = _passwordHasher.HashPassword(employee, "Zavrtnica.17!");
        employee.SecurityStamp = Guid.NewGuid().ToString();
        employee.PasswordHash = hashedPassword;

        var result = await _userManager.CreateAsync(employee);
        if (!result.Succeeded)
        {
            List<IdentityError> errorList = result.Errors.ToList();
            var errors = string.Join(", ", errorList.Select(e=>e.Description));
            throw new IdentityResultException(errors);
        }
    }

    public async Task<List<Employee>> GetEmployeesForIndex()
    {
        return await _context.Employees
            .Include(x => x.Department).ThenInclude(y => y.Company)
            .Where(x => x.UserRoles.FirstOrDefault().RoleId == (int)RoleEnums.User)
            .ToListAsync();
    }

    public async Task<Employee> GetEmployeeForDetailsByIdAsync(int id)
    {
        return await _context.Employees
            .Include(x => x.Department).ThenInclude(y => y.Company)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task DeleteEmployeeAsync(int id)
    {
        var employee = await GetEmployeeForDetailsByIdAsync(id);
        await _userManager.DeleteAsync(employee);
    }


    public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
    {
        return await _context.Employees
            .SingleOrDefaultAsync(x => x.Id == employeeId);
    }

    public async Task<List<SelectListItem>> GetEmployeesSelectListAsync()
    {
        var employees = await _context.Employees.ToListAsync();
        return employees.ToSelectList(x => x.Id, x => x.FirstName + " " + x.LastName);
    }

    public async Task<List<SelectListItem>> GetCompaniesSelectListAsync()
    {
        var companies = await _context.Companies.ToListAsync();
        return companies.ToSelectList(x => x.Id, x => x.Name);
    }

    public async Task<List<SelectListItem>> GetDepartmentsSelectListAsync()
    {
        var departments = await _context.Departments.Where(x => !x.IsDeleted).ToListAsync();
        return departments.ToSelectList(x => x.Id, x => x.Name);
    }

    public async Task<List<SelectListItem>> GetDepartmentsSelectListAsync(int companyId)
    {
        var departments = await _context.Departments.Where(x => x.CompanyId == companyId).ToListAsync();
        return departments.ToSelectList(x => x.Id, x => x.Name);
    }

    public async Task EditCompanyAsync(EditCompanyViewModel viewModel)
    {
        var company = await GetCompanyForDetailsAsync(viewModel.Id);

        company.Name = viewModel.Name;
        company.Description = viewModel.Description;
    }

    public async Task EditDepartmentAsync(EditDepartmentViewModel viewModel)
    {
        var department = await GetDepartmentForDetailsAsync(viewModel.Id);
        department.Name = viewModel.Name;
        department.Description = viewModel.Description;
        
    }

    public async Task EditEmployeeAsync(EditEmployeeViewModel viewModel)
    {
        var employee = await GetEmployeeForDetailsByIdAsync(viewModel.Id);
        employee.FirstName = viewModel.FirstName;
        employee.LastName = viewModel.LastName;
        employee.NumberOfVacationDaysPerYear = viewModel.NumberOfVacationDaysPerYear;
        employee.DepartmentId = viewModel.DepartmentId;
    }
}