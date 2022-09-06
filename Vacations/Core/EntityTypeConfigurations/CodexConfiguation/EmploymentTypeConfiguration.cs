using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Vacations.Core.Models.Codex;

namespace Vacations.Core.EntityTypeConfigurations.CodexConfiguation
{
    public class EmploymentTypeConfiguration : IEntityTypeConfiguration<EmploymentType>
    {
        public void Configure(EntityTypeBuilder<EmploymentType> builder)
        {
            builder.Property(d => d.Name)
                .IsRequired();

            builder.Property(d => d.Description)
                .IsRequired();

            builder.ToTable("EmploymentType", "codex");
        }

    }
}