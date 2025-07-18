using NotificationService.Domain.Repositories;

namespace NotificationService.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        INotificationRepository Notifications { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}