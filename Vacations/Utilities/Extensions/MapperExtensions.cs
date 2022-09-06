using System.Collections.Generic;
using Vacations.Core.Models.Vacation;

namespace Vacations.Utilities.Extensions
{
    public static class MapperExtensions
    {

        public static List<string> MapReplacementEmployees(this VacationRequest vacationRequest)
        {
            List<string> employees = new List<string>();

            if (vacationRequest == null || vacationRequest.VacationRequestReplacementEmployees == null)
            {
                return employees;
            }

            foreach (var replacementEmployee in vacationRequest.VacationRequestReplacementEmployees)
            {
                employees.Add(replacementEmployee.Employee.FirstName + " " + replacementEmployee.Employee.LastName);
            }
            
            return employees;
        }

    }
}
