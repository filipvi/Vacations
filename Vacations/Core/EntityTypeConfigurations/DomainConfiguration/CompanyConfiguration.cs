using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Vacations.Core.Models.Domain;

namespace Vacations.Core.EntityTypeConfigurations.DomainConfiguration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(d => d.Name)
                .IsRequired();

            builder.Property(d => d.Description)
                .IsRequired();

            builder.HasMany(c => c.Departments)
                .WithOne(d => d.Company)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Company", "domain");
        }

    }
}