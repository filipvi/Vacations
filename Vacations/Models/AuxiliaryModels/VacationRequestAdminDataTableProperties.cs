namespace Vacations.Models.AuxiliaryModels;

public class VacationRequestAdminDataTableProperties : DataTableProperties
{
    public string SearchDateFrom { get; set; }
    public string SearchDateTo { get; set; }
    public string SearchYear { get; set; }
    public string SearchEmployee { get; set; }
    public string SearchCompany { get; set; }
    public string SearchDepartment { get; set; }
}