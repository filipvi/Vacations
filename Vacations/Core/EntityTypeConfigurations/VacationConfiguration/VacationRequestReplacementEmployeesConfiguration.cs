using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vacations.Core.Models.Vacation;

namespace Vacations.Core.EntityTypeConfigurations.VacationConfiguration;

public class VacationRequestReplacementEmployeesConfiguration : IEntityTypeConfiguration<VacationRequestReplacementEmployees>
{
    public void Configure(EntityTypeBuilder<VacationRequestReplacementEmployees> builder)
    {
        builder.ToTable("VacationRequestReplacementEmployees", "dbo");
    }
}