using System.Threading.Tasks;
using Vacations.Core;

namespace Vacations.Models.ViewModels.Request;

public class NoAvailableDaysViewModel
{
    public int EmployeeId { get; set; }
    public int AvailableDaysCurrentYear { get; set; }
    public int UsedDaysCurrentYear { get; set; }
    public string Message { get; set; }

    public async Task PrepareData(IUnitOfWork unitOfWork)
    {
        UsedDaysCurrentYear = await unitOfWork.RequestRepository
            .GetUsedDaysOfVacationCurrentYearAsync(EmployeeId);
        AvailableDaysCurrentYear = await unitOfWork.RequestRepository
            .GetAvailableDaysForEmployeeAsync(EmployeeId);
        
        if (AvailableDaysCurrentYear - UsedDaysCurrentYear == 0)
        {
            Message =
                "You don't have more available days in current year. Head to my requests if you want to delete some!";
        }
    }
}