using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Vacations.Core.Models.Codex;

namespace Vacations.Core.EntityTypeConfigurations.CodexConfiguation
{
    public class VacationRequestStatusConfiguration : IEntityTypeConfiguration<VacationRequestStatus>
    {
        public void Configure(EntityTypeBuilder<VacationRequestStatus> builder)
        {
            builder.Property(d => d.Name)
                .IsRequired();

            builder.Property(d => d.Description)
                .IsRequired();

            builder.ToTable("VacationRequestStatus", "codex");
        }

    }
}