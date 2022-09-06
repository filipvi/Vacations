using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Vacations.Core;
using Vacations.Models;
using Vacations.Models.Exceptions;
using Vacations.Models.Results;
using Vacations.Models.ViewModels.Domain;
using Vacations.Utilities.Extensions;
using Vacations.Utilities.Logger;
using Vacations.Utilities.Security;
using Z.EntityFramework.Plus;

namespace Vacations.Controllers;

[Authorize(Roles = UserRoles.Administrator)]
public class DomainController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DomainController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    #region Companies

    // Index
    public async Task<IActionResult> Companies()
    {
        var viewModel = new IndexCompanyViewModel();

        try
        {
            await viewModel.PrepareData(_unitOfWork, _mapper);
        }
        catch (Exception e)
        {
            Log.ControllerLog(this, e, null);
            var errorModel = new ErrorViewModel
            {
                Message = "Error fetching data",
                Url = Request.GetDisplayUrl(),
                UserName = User.GetLoggedInUserName()
            };

            return View("Error", errorModel);
        }

        return View(viewModel);
    }


    // Details
    public async Task<IActionResult> DetailsCompany(int id)
    {
        var viewModel = new DetailsCompanyViewModel {Id = id};

        try
        {
            await viewModel.PrepareData(_unitOfWork, _mapper);
        }
        catch (Exception e)
        {
            Log.ControllerLog(this, e, null);
            var errorModel = new ErrorViewModel
            {
                Message = "Error fetching data",
                Url = Request.GetDisplayUrl(),
                UserName = User.GetLoggedInUserName()
            };

            return View("Error", errorModel);
        }

        return View(viewModel);
    }


    // Create
    [HttpGet]
    public IActionResult CreateCompany()
    {
        var viewModel = new CreateCompanyViewModel { };

        try
        {
            return View(viewModel);
        }
        catch (Exception e)
        {
            Log.ControllerLog(this, e, null);
            var errorModel = new ErrorViewModel
            {
                Message = "Error fetching data",
                Url = Request.GetDisplayUrl(),
                UserName = User.GetLoggedInUserName()
            };

            return View("Error", errorModel);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCompany(CreateCompanyViewModel viewModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new ModelNotValidException("Data not entered correctly!");
            }

            var audit = new Audit {CreatedBy = User.GetLoggedInUserName()};
            await _unitOfWork.DomainRepository.CreateCompanyAsync(viewModel);
            await _unitOfWork.CompleteAsync(audit);

            TempData["SuccessMsg"] = "New company created successfuly";
            return RedirectToAction(nameof(Companies));
        }
        catch (Exception e)
        {
            if (e is ModelNotValidException)
            {
                TempData["ErrorMsg"] = e.Message;
            }
            else
            {
                Log.ControllerLog(this, e, null);
                TempData["ErrorMsg"] = "Error while saving data";
            }

            return View(viewModel);
        }
    }

    // Edit
    [HttpGet]
    public async Task<IActionResult> EditCompany(int id)
    {
        var viewModel = new EditCompanyViewModel {Id = id};

        try
        {
            await viewModel.PrepareData(_unitOfWork);
            return View(viewModel);
        }
        catch (Exception e)
        {
            Log.ControllerLog(this, e, null);
            var errorModel = new ErrorViewModel
            {
                Message = "Error fetching data",
                Url = Request.GetDisplayUrl(),
                UserName = User.GetLoggedInUserName()
            };

            return View("Error", errorModel);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditCompany(EditCompanyViewModel viewModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new ModelNotValidException("Data not entered correctly!");
            }

            var audit = new Audit {CreatedBy = User.GetLoggedInUserName()};
            await _unitOfWork.DomainRepository.EditCompanyAsync(viewModel);
            await _unitOfWork.CompleteAsync(audit);

            TempData["SuccessMsg"] = "Company updated successfuly";
            return RedirectToAction("DetailsCompany", "Domain", new {id = viewModel.Id});
        }
        catch (Exception e)
        {
            if (e is ModelNotValidException)
            {
                TempData["ErrorMsg"] = e.Message;
            }
            else
            {
                Log.ControllerLog(this, e, null);
                TempData["ErrorMsg"] = "Error while saving data";
            }

            return View(viewModel);
        }
    }


    // Delete
    public async Task<JsonResult> DeleteCompany(int id)
    {
        var result = new Result();

        try
        {
            var audit = new Audit {CreatedBy = User.GetLoggedInUserName()};
            await _unitOfWork.DomainRepository.DeleteCompanyAsync(id);
            await _unitOfWork.CompleteAsync(audit);

            result.Success = true;
            TempData["SuccessMsg"] = "Company deleted successfuly";
        }
        catch (Exception e)
        {
            result.Success = false;

            if (e is DeleteNotAllowedException)
            {
                result.Message = e.Message;
            }

            result.Message = "Error while deleting company";
        }

        return Json(result);
    }

    #endregion Companies

    #region Departments

    // Index
    public async Task<IActionResult> Departments()
    {
        var viewModel = new IndexDepartmentViewModel();

        try
        {
            await viewModel.PrepareData(_unitOfWork, _mapper);
        }
        catch (Exception e)
        {
            Log.ControllerLog(this, e, null);
            var errorModel = new ErrorViewModel
            {
                Message = "Error fetching data",
                Url = Request.GetDisplayUrl(),
                UserName = User.GetLoggedInUserName()
            };

            return View("Error", errorModel);
        }

        return View(viewModel);
    }


    // Details
    public async Task<IActionResult> DetailsDepartment(int id)
    {
        var viewModel = new DetailsDepartmentViewModel {Id = id};

        try
        {
            await viewModel.PrepareData(_unitOfWork, _mapper);
        }
        catch (Exception e)
        {
            Log.ControllerLog(this, e, null);
            var errorModel = new ErrorViewModel
            {
                Message = "Error fetching data",
                Url = Request.GetDisplayUrl(),
                UserName = User.GetLoggedInUserName()
            };

            return View("Error", errorModel);
        }

        return View(viewModel);
    }


    // Create
    [HttpGet]
    public async Task<IActionResult> CreateDepartment(int companyId)
    {
        var viewModel = new CreateDepartmentViewModel {CompanyId = companyId};

        try
        {
            await viewModel.PrepareData(_unitOfWork);
            return View(viewModel);
        }
        catch (Exception e)
        {
            Log.ControllerLog(this, e, null);
            var errorModel = new ErrorViewModel
            {
                Message = "Error fetching data",
                Url = Request.GetDisplayUrl(),
                UserName = User.GetLoggedInUserName()
            };

            return View("Error", errorModel);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateDepartment(CreateDepartmentViewModel viewModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new ModelNotValidException("Data not entered correctly!");
            }

            var audit = new Audit {CreatedBy = User.GetLoggedInUserName()};
            await _unitOfWork.DomainRepository.CreateDepartmentAsync(viewModel);
            await _unitOfWork.CompleteAsync(audit);

            TempData["SuccessMsg"] = "New department created successfuly";
            return RedirectToAction("DetailsCompany", new {id = viewModel.CompanyId});
        }
        catch (Exception e)
        {
            if (e is ModelNotValidException)
            {
                TempData["ErrorMsg"] = e.Message;
            }
            else
            {
                Log.ControllerLog(this, e, null);
                TempData["ErrorMsg"] = "Error while saving data";
            }

            return View(viewModel);
        }
    }

    // Edit
    [HttpGet]
    public async Task<IActionResult> EditDepartment(int departmentId)
    {
        var viewModel = new EditDepartmentViewModel {Id = departmentId};

        try
        {
            await viewModel.PrepareData(_unitOfWork);
            return View(viewModel);
        }
        catch (Exception e)
        {
            Log.ControllerLog(this, e, null);
            var errorModel = new ErrorViewModel
            {
                Message = "Error fetching data",
                Url = Request.GetDisplayUrl(),
                UserName = User.GetLoggedInUserName()
            };

            return View("Error", errorModel);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditDepartment(EditDepartmentViewModel viewModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new ModelNotValidException("Data not entered correctly!");
            }

            var audit = new Audit {CreatedBy = User.GetLoggedInUserName()};
            await _unitOfWork.DomainRepository.EditDepartmentAsync(viewModel);
            await _unitOfWork.CompleteAsync(audit);

            TempData["SuccessMsg"] = "Department updated successfuly";
            return RedirectToAction("DetailsDepartment", new {id = viewModel.Id});
        }
        catch (Exception e)
        {
            if (e is ModelNotValidException)
            {
                TempData["ErrorMsg"] = e.Message;
            }
            else
            {
                Log.ControllerLog(this, e, null);
                TempData["ErrorMsg"] = "Error while saving data";
            }

            return View(viewModel);
        }
    }

    // Delete
    public async Task<JsonResult> DeleteDepartment(int id)
    {
        var result = new Result();

        try
        {
            var audit = new Audit {CreatedBy = User.GetLoggedInUserName()};
            await _unitOfWork.DomainRepository.DeleteDepartmentAsync(id);
            await _unitOfWork.CompleteAsync(audit);

            result.Success = true;
            TempData["SuccessMsg"] = "Department deleted successfuly";
        }
        catch (Exception e)
        {
            result.Success = false;

            if (e is DeleteNotAllowedException)
            {
                result.Message = e.Message;
            }

            result.Message = "Error while deleting department";
        }

        return Json(result);
    }

    #endregion Departments

    #region Employees

    // Index
    public async Task<IActionResult> Employees()
    {
        var viewModel = new IndexEmployeeViewModel();

        try
        {
            await viewModel.PrepareData(_unitOfWork, _mapper);
        }
        catch (Exception e)
        {
            Log.ControllerLog(this, e, null);
            var errorModel = new ErrorViewModel
            {
                Message = "Error fetching data",
                Url = Request.GetDisplayUrl(),
                UserName = User.GetLoggedInUserName()
            };

            return View("Error", errorModel);
        }

        return View(viewModel);
    }


    // Details
    public async Task<IActionResult> DetailsEmployee(int id)
    {
        var viewModel = new DetailsEmployeeViewModel {Id = id};

        try
        {
            await viewModel.PrepareData(_unitOfWork, _mapper);
        }
        catch (Exception e)
        {
            Log.ControllerLog(this, e, null);
            var errorModel = new ErrorViewModel
            {
                Message = "Error fetching data",
                Url = Request.GetDisplayUrl(),
                UserName = User.GetLoggedInUserName()
            };

            return View("Error", errorModel);
        }

        return View(viewModel);
    }


    // Create
    [HttpGet]
    public async Task<IActionResult> CreateEmployee(int departmentId)
    {
        var viewModel = new CreateEmployeeViewModel {DepartmentId = departmentId};

        try
        {
            await viewModel.PrepareData(_unitOfWork);
            return View(viewModel);
        }
        catch (Exception e)
        {
            Log.ControllerLog(this, e, null);
            var errorModel = new ErrorViewModel
            {
                Message = "Error fetching data",
                Url = Request.GetDisplayUrl(),
                UserName = User.GetLoggedInUserName()
            };

            return View("Error", errorModel);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateEmployee(CreateEmployeeViewModel viewModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new ModelNotValidException("Data not entered correctly!");
            }

            var audit = new Audit {CreatedBy = User.GetLoggedInUserName()};
            await _unitOfWork.DomainRepository.CreateEmployeeAsync(viewModel);
            await _unitOfWork.CompleteAsync(audit);

            TempData["SuccessMsg"] = "New employee created successfuly";
            return RedirectToAction("DetailsDepartment", new {id = viewModel.DepartmentId});
        }
        catch (Exception e)
        {
            if (e is ModelNotValidException || e is IdentityResultException)
            {
                TempData["ErrorMsg"] = e.Message;
            }
            else
            {
                Log.ControllerLog(this, e, null);
                TempData["ErrorMsg"] = "Error while saving data";
            }

            return View(viewModel);
        }
    }

    // Edit
    [HttpGet]
    public async Task<IActionResult> EditEmployee(int employeeId)
    {
        var viewModel = new EditEmployeeViewModel {Id = employeeId};

        try
        {
            await viewModel.PrepareData(_unitOfWork);
            await viewModel.PrepareSelectLists(_unitOfWork);
            return View(viewModel);
        }
        catch (Exception e)
        {
            Log.ControllerLog(this, e, null);
            var errorModel = new ErrorViewModel
            {
                Message = "Error fetching data",
                Url = Request.GetDisplayUrl(),
                UserName = User.GetLoggedInUserName()
            };

            return View("Error", errorModel);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditEmployee(EditEmployeeViewModel viewModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new ModelNotValidException("Data not entered correctly!");
            }

            var audit = new Audit {CreatedBy = User.GetLoggedInUserName()};
            await _unitOfWork.DomainRepository.EditEmployeeAsync(viewModel);
            await _unitOfWork.CompleteAsync(audit);

            TempData["SuccessMsg"] = "Employee updated successfuly";
            return RedirectToAction("DetailsEmployee", new {id = viewModel.Id});
        }
        catch (Exception e)
        {
            if (e is ModelNotValidException || e is IdentityResultException)
            {
                TempData["ErrorMsg"] = e.Message;
            }
            else
            {
                Log.ControllerLog(this, e, null);
                TempData["ErrorMsg"] = "Error while saving data";
            }

            return View(viewModel);
        }
    }
    
    [HttpPost]
    public async Task<PartialViewResult> GetDepartmentsSelectList(int companyId)
    {
        var viewModel = new EditEmployeeViewModel();
        
        try
        {
            await viewModel.PrepareDepartmentsSelectList(companyId, _unitOfWork);
        }
        catch (Exception e)
        {
            Log.ControllerLog(this,e,companyId);
        }

        return PartialView("_EditEmployeeDepartmentSelectList", viewModel);
    }


    // Delete
    public async Task<JsonResult> DeleteEmployee(int id)
    {
        var result = new Result();

        try
        {
            var audit = new Audit {CreatedBy = User.GetLoggedInUserName()};
            await _unitOfWork.DomainRepository.DeleteEmployeeAsync(id);
            await _unitOfWork.CompleteAsync(audit);

            result.Success = true;
            TempData["SuccessMsg"] = "Employee deleted successfuly";
        }
        catch (Exception e)
        {
            result.Success = false;

            if (e is DeleteNotAllowedException)
            {
                result.Message = e.Message;
            }

            result.Message = "Error while deleting employee";
        }

        return Json(result);
    }

    #endregion Employees

   
}