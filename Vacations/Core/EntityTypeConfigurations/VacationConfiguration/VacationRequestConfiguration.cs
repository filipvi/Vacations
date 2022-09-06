using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Vacations.Core.Models.Vacation;

namespace Vacations.Core.EntityTypeConfigurations.VacationConfiguration
{
    public class VacationRequestConfiguration : IEntityTypeConfiguration<VacationRequest>
    {
        public void Configure(EntityTypeBuilder<VacationRequest> builder)
        {
            builder.Property(d => d.YearInfo)
                .IsRequired();

            builder.Property(d => d.Description)
                .IsRequired();

            builder.HasOne(c => c.Employee)
                .WithMany(hg => hg.VacationRequests)
                .HasForeignKey(c => c.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.VacationRequestStatus)
                .WithMany()
                .HasForeignKey(c => c.VacationRequestStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(n => n.VacationRequestResponses)
                .WithOne(o => o.VacationRequest)
                .HasForeignKey(o => o.VacationRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(n => n.VacationRequestReplacementEmployees)
                .WithOne(o => o.VacationRequest)
                .HasForeignKey(o => o.VacationRequestId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.ToTable("VacationRequest", "dbo");
        }

    }
}