namespace Vacations.Models.ViewModels.Request;

public class DetailsVacationRequestResponseViewModel
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int StatusId { get; set; }
    public string DateCreated { get; set; }
    public string Employee { get; set; }
    public string Status { get; set; }
}