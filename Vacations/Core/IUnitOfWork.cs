using System.Threading.Tasks;
using Vacations.Core.Interfaces;
using Z.EntityFramework.Plus;

namespace Vacations.Core
{
    public interface IUnitOfWork
    {
        IDomainRepository DomainRepository { get; }
        IRequestRepository RequestRepository { get; }

        Task CompleteAsync();
        Task CompleteAsync(Audit audit);

        void Complete();
        void Complete(Audit audit);
    }
}