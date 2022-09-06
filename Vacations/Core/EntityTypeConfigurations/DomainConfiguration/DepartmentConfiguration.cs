using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Vacations.Core.Models.Domain;

namespace Vacations.Core.EntityTypeConfigurations.DomainConfiguration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(d => d.Name)
                .IsRequired();

            builder.Property(d => d.Description)
                .IsRequired();

            
            builder.HasMany(c => c.Employees)
                .WithOne(d => d.Department)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Company)
                .WithMany(hg => hg.Departments)
                .HasForeignKey(c => c.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Department", "domain");
        }

    }
}