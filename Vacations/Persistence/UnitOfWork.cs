using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Vacations.Core;
using Vacations.Core.Interfaces;
using Vacations.Core.Models.Identity;
using Vacations.Persistence.Repositories;
using Z.EntityFramework.Plus;

namespace Vacations.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<Employee> _passwordHasher;
        private readonly UserManager<Employee> _userManager;

        public IDomainRepository DomainRepository { get; private set; }
        public IRequestRepository RequestRepository { get; private set; }


        public UnitOfWork(ApplicationDbContext context, IPasswordHasher<Employee> passwordHasher, UserManager<Employee> userManager)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _userManager = userManager;
            DomainRepository = new DomainRepository(context, passwordHasher, userManager);
            RequestRepository = new RequestRepository(context);

        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task CompleteAsync(Audit audit)
        {
            await _context.SaveChangesAsync(audit);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public void Complete(Audit audit)
        {
            _context.SaveChanges(audit);
        }
    }
}
