using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Vacations.Core.Models.Vacation;

namespace Vacations.Core.EntityTypeConfigurations.VacationConfiguration
{
    public class VacationRequestResponseConfiguration : IEntityTypeConfiguration<VacationRequestResponse>
    {
        public void Configure(EntityTypeBuilder<VacationRequestResponse> builder)
        {
            builder.HasOne(c => c.VacationRequestStatus)
                .WithMany()
                .HasForeignKey(c => c.VacationRequestStatusId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(c => c.Employee)
                .WithMany()
                .HasForeignKey(c => c.EmployeeCreatedId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("VacationRequestResponse", "dbo");
        }

    }
}