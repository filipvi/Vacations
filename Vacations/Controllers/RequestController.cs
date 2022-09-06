using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Vacations.Core;
using Vacations.Models;
using Vacations.Models.Enums;
using Vacations.Models.Exceptions;
using Vacations.Models.Results;
using Vacations.Models.ViewModels.Request;
using Vacations.Utilities.Extensions;
using Vacations.Utilities.Logger;
using Vacations.Utilities.Security;
using Z.EntityFramework.Plus;

namespace Vacations.Controllers;

[AuthorizeRoles(UserRoles.Administrator, UserRoles.User)]
public class RequestController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RequestController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // EMPLOYEE

    #region Create

    [AuthorizeRoles(UserRoles.User)]
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var viewModel = new CreateVacationRequestViewModel();

        try
        {
            var employeeId = User.GetLoggedInUserId<int>();
            await viewModel.PrepareData(employeeId, _unitOfWork, _mapper);
            await viewModel.PrepareSelectLists(_unitOfWork);
        }
        catch (Exception e)
        {
            var errorModel = new ErrorViewModel
            {
                Message = "Error fetching data",
                Url = Request.GetDisplayUrl(),
                UserName = User.GetLoggedInUserName()
            };

            if (e is ModelNotValidException)
            {
                return RedirectToAction("NoAvailableDays", "Request");
            }

            Log.ControllerLog(this, e, null);
            errorModel.Message = "Error fetching data";
            return View("Error", errorModel);
        }

        return View(viewModel);
    }

    [AuthorizeRoles(UserRoles.User)]
    [HttpPost]
    public async Task<IActionResult> Create(CreateVacationRequestViewModel viewModel)
    {
        try
        {
            await viewModel.PrepareDataForSaving(_unitOfWork);
            var audit = new Audit {CreatedBy = User.GetLoggedInUserName()};
            await _unitOfWork.RequestRepository.CreateVacationRequestAsync(viewModel.VacationRequest);
            await _unitOfWork.CompleteAsync(audit);

            TempData["SuccessMsg"] = "New vacation request submitted successfuly";
        }
        catch (Exception e)
        {
            var errorModel = new ErrorViewModel
            {
                Message = "Error saving vacation request",
                Url = Request.GetDisplayUrl(),
                UserName = User.GetLoggedInUserName()
            };

            if (e is ModelNotValidException)
            {
                TempData["ErrorMsg"] = e.Message;
                return View(viewModel);
            }

            Log.ControllerLog(this, e, null);
            errorModel.Message = "Error saving vacation request";
            return View("Error", errorModel);
        }

        return RedirectToAction("MyVacationRequests", "Request");
    }

    #endregion Create

    #region Details

    [AuthorizeRoles(UserRoles.User)]
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var viewModel = new DetailsVacationRequestViewModel
        {
            Id = id,
            EmployeeId = User.GetLoggedInUserId<int>()
        };

        try
        {
            await viewModel.PrepareData(_unitOfWork, _mapper);
            return View(viewModel);
        }
        catch (Exception exc)
        {
            var errorModel = new ErrorViewModel
            {
                Url = Url.Action("Details", "Request"),
                UserName = User.GetLoggedInUserName(),
            };

            if (exc is UserNotAllowedException || exc is RecordNotFoundException)
            {
                errorModel.Message = exc.Message;
            }
            else
            {
                Log.ControllerLog(this, exc, id);
                errorModel.Message = "Error fetching data";
            }

            return View("Error", errorModel);
        }
    }

    #endregion Details

    #region My requests

    [AuthorizeRoles(UserRoles.User)]
    public async Task<IActionResult> MyVacationRequests()
    {
        var viewModel = new IndexVacationRequestViewModel
        {
            EmployeeId = User.GetLoggedInUserId<int>()
        };

        await viewModel.PrepareSelectLists(_unitOfWork);

        return View(viewModel);
    }

    [HttpPost]
    [AuthorizeRoles(UserRoles.User)]
    public async Task<JsonResult> GetRequestsData()
    {
        try
        {
            IFormCollection form = await Request.ReadFormAsync();
            IndexVacationRequestViewModel viewModel = new IndexVacationRequestViewModel();
            await viewModel.GetData(form, _unitOfWork, _mapper);

            var result = Json(new
            {
                draw = Convert.ToInt32(viewModel.Draw),
                recordsTotal = viewModel.TotalRecords,
                recordsFiltered = viewModel.RecFilter,
                data = viewModel.RequestList
            });

            return result;
        }
        catch (Exception e)
        {
            Log.ControllerLog(this, e, null);

            var result = Json(new
            {
                draw = 0,
                recordsTotal = 0,
                recordsFiltered = 0,
                data = 0,
                error = "Error fetching data"
            });
            return result;
        }
    }

    #endregion My requests

    #region Edit

    [AuthorizeRoles(UserRoles.User)]
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var viewModel = new EditVacationRequestViewModel
        {
            Id = id,
            EmployeeId = User.GetLoggedInUserId<int>()
        };

        try
        {
            await viewModel.PrepareData(_unitOfWork, _mapper);
            await viewModel.PrepareSelectLists(_unitOfWork);
            return View(viewModel);
        }
        catch (Exception exc)
        {
            var errorModel = new ErrorViewModel
            {
                Url = Url.Action("Details", "Request"),
                UserName = User.GetLoggedInUserName(),
            };

            if (exc is UserNotAllowedException)
            {
                errorModel.Message = exc.Message;
            }
            else
            {
                Log.ControllerLog(this, exc, id);
                errorModel.Message = "Error fetching data";
            }

            return View("Error", errorModel);
        }
    }

    [AuthorizeRoles(UserRoles.User)]
    [HttpPost]
    public async Task<IActionResult> Edit(EditVacationRequestViewModel viewModel)
    {
        try
        {
            var audit = new Audit {CreatedBy = User.GetLoggedInUserName()};
            await _unitOfWork.RequestRepository.EditVacationRequestAsync(viewModel);
            await _unitOfWork.CompleteAsync(audit);

            TempData["SuccessMsg"] = "Vacation request updated successfuly";
        }
        catch (Exception e)
        {
            var errorModel = new ErrorViewModel
            {
                Url = Request.GetDisplayUrl(),
                UserName = User.GetLoggedInUserName()
            };

            if (e is UserNotAllowedException)
            {
                errorModel.Message = e.Message;
            }
            else
            {
                errorModel.Message = "Error updating vacation request";
                Log.ControllerLog(this, e, null);
            }


            return View("Error", errorModel);
        }

        return RedirectToAction("MyVacationRequests", "Request");
    }

    #endregion Edit

    #region Delete

    [AuthorizeRoles(UserRoles.User, UserRoles.Administrator)]
    public async Task<JsonResult> Delete(int id)
    {
        var result = new Result();

        try
        {
            var audit = new Audit {CreatedBy = User.GetLoggedInUserName()};
            await _unitOfWork.RequestRepository.DeleteRequestAsync(id, User.GetLoggedInUserId<int>());
            await _unitOfWork.CompleteAsync(audit);

            result.Success = true;
            TempData["SuccessMsg"] = "Delete successful";
        }
        catch (Exception e)
        {
            result.Success = false;

            if (e is DeleteNotAllowedException || e is UserNotAllowedException)
            {
                result.Message = e.Message;
            }
            else
            {
                Log.ControllerLog(this, e, null);
                result.Message = "Error while deleting request";
            }
        }

        return Json(result);
    }

    #endregion Delete

    #region No available days

    [AuthorizeRoles(UserRoles.User)]
    public async Task<IActionResult> NoAvailableDays()
    {
        var viewModel = new NoAvailableDaysViewModel
        {
            EmployeeId = User.GetLoggedInUserId<int>()
        };

        await viewModel.PrepareData(_unitOfWork);
        return View(viewModel);
    }

    #endregion No available days


    // ADMIN

    #region Requests

    [AuthorizeRoles(UserRoles.Administrator)]
    public async Task<IActionResult> Pending()
    {
        var viewModel = new IndexVacationRequestAdminViewModel
        {
            StatusId = (int) VacationRequestStatusEnums.Pending
        };

        await viewModel.PrepareSelectLists(_unitOfWork);

        return View(viewModel);
    }

    [AuthorizeRoles(UserRoles.Administrator)]
    public async Task<IActionResult> Approved()
    {
        var viewModel = new IndexVacationRequestAdminViewModel
        {
            StatusId = (int) VacationRequestStatusEnums.Approved
        };
        await viewModel.PrepareSelectLists(_unitOfWork);

        return View(viewModel);
    }

    [AuthorizeRoles(UserRoles.Administrator)]
    public async Task<IActionResult> Invalid()
    {
        var viewModel = new IndexVacationRequestAdminViewModel
        {
            StatusId = (int) VacationRequestStatusEnums.Invalid
        };
        await viewModel.PrepareSelectLists(_unitOfWork);

        return View(viewModel);
    }

    [AuthorizeRoles(UserRoles.Administrator)]
    public async Task<IActionResult> Rejected()
    {
        var viewModel = new IndexVacationRequestAdminViewModel
        {
            StatusId = (int) VacationRequestStatusEnums.Rejected
        };
        await viewModel.PrepareSelectLists(_unitOfWork);

        return View(viewModel);
    }

    [HttpPost]
    [AuthorizeRoles(UserRoles.Administrator)]
    public async Task<IActionResult> GetAdminRequests(int statusId)
    {
        try
        {
            IFormCollection form = await Request.ReadFormAsync();
            IndexVacationRequestAdminViewModel viewModel = new IndexVacationRequestAdminViewModel();
            await viewModel.GetData(form, statusId, _unitOfWork, _mapper);

            var result = Json(new
            {
                draw = Convert.ToInt32(viewModel.Draw),
                recordsTotal = viewModel.TotalRecords,
                recordsFiltered = viewModel.RecFilter,
                data = viewModel.RequestList
            });

            return result;
        }
        catch (Exception e)
        {
            Log.ControllerLog(this, e, null);

            var result = Json(new
            {
                draw = 0,
                recordsTotal = 0,
                recordsFiltered = 0,
                data = 0,
                error = "Error fetching data"
            });
            return result;
        }
    }

    #endregion Requests

    #region Admin details

    public async Task<IActionResult> AdminDetails(int id)
    {
        var viewModel = new DetailsVacationRequestAdminViewModel
        {
            Id = id
        };

        try
        {
            await viewModel.PrepareData(_unitOfWork, _mapper);
            return View(viewModel);
        }
        catch (Exception e)
        {
            Log.ControllerLog(this, e, id);
            var errorModel = new ErrorViewModel
            {
                Message = "Error fetching data",
                Url = Request.GetDisplayUrl(),
                UserName = User.GetLoggedInUserName()
            };

            return View("Error", errorModel);
        }
    }

    #endregion Admin details

    #region Change status

    [HttpPost]
    public async Task<IActionResult> GetChangeStatusModal(int id)
    {
        var viewModel = new ChangeStatusViewModel {Id = id};

        try
        {
            await viewModel.PopulateDataAsync(_unitOfWork);
            await viewModel.PrepareSelectLists(_unitOfWork);
            return PartialView("_ChangeStatusModal", viewModel);
        }
        catch (Exception e)
        {
            Log.ControllerLog(this, e, id);
            return BadRequest();
        }
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<JsonResult> ChangeStatus(ChangeStatusViewModel viewModel)
    {
        var result = new Result();

        if (!ModelState.IsValid)
        {
            result.Success = false;
            result.Message = "Error validating data";
            return Json(result);
        }

        try
        {
            var audit = new Audit {CreatedBy = User.GetLoggedInUserId()};
            await _unitOfWork.RequestRepository.ChangeStatusAsync(viewModel, User.GetLoggedInUserId<int>());
            await _unitOfWork.CompleteAsync(audit);

            TempData["SuccessMsg"] = "Vacation request updated successfuly";
            result.Success = true;
        }
        catch (Exception e)
        {
            Log.ControllerLog(this, e, viewModel.Id);

            result.Message = "Error changing vacation request status";
            result.Success = false;
        }

        return Json(result);
    }

    #endregion Change status
}