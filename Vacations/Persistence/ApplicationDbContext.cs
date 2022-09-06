using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vacations.Core.EntityTypeConfigurations.CodexConfiguation;
using Vacations.Core.EntityTypeConfigurations.DomainConfiguration;
using Vacations.Core.EntityTypeConfigurations.UserTypeConfigurations;
using Vacations.Core.EntityTypeConfigurations.VacationConfiguration;
using Vacations.Core.Models.Codex;
using Vacations.Core.Models.Domain;
using Vacations.Core.Models.Identity;
using Vacations.Core.Models.Vacation;
using Z.EntityFramework.Plus;

namespace Vacations.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<Employee, ApplicationRole, int, IdentityUserClaim<int>,
        EmployeeRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        // Audit Models
        public DbSet<AuditEntry> AuditEntries { get; set; }
        public DbSet<AuditEntryProperty> AuditEntryProperties { get; set; }



        // Identity Models
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ApplicationRole> EmployeeRoles { get; set; }

        

        // codex data - alphabetic order
        public DbSet<EmploymentType> EmployementTypes { get; set; }
        public DbSet<VacationRequestStatus> VacationRequestStatuses { get; set; }



        // domain data - alphabetic order
        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments{ get; set; }


        // business data 
        public DbSet<VacationRequest> VacationRequests{ get; set; }
        public DbSet<VacationRequestResponse> VacationRequestResponses { get; set; }



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // AUDIT
            AuditManager.DefaultConfiguration.AutoSavePreAction = (context, audit) =>
                // ADD "Where(x => x.AuditEntryID == 0)" to allow multiple SaveChanges with same Audit
                ((ApplicationDbContext)context).AuditEntries.AddRange(audit.Entries);

            AuditManager.DefaultConfiguration.ExcludeDataAnnotation();
            AuditManager.DefaultConfiguration.DataAnnotationDisplayName();

            // IDENTITY
            modelBuilder.ApplyConfiguration(new ApplicationUserRoleConfiguration());

            // CODEX
            modelBuilder.ApplyConfiguration(new EmploymentTypeConfiguration());
            modelBuilder.ApplyConfiguration(new VacationRequestStatusConfiguration());

            // DOMAIN
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());

            // BUSSINESS
            modelBuilder.ApplyConfiguration(new VacationRequestConfiguration());
            modelBuilder.ApplyConfiguration(new VacationRequestResponseConfiguration());
        }
    }
}
