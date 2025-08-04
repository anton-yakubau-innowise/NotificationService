using System.Linq.Expressions;
using NotificationService.Domain.Entities;

namespace NotificationService.Domain.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Notification?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Notification>> ListAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Notification>> ListAllAsNoTrackingAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Notification>> ListAsync(Expression<Func<Notification, bool>> predicate, CancellationToken cancellationToken = default);
        Task<IEnumerable<Notification>> ListAsNoTrackingAsync(Expression<Func<Notification, bool>> predicate, CancellationToken cancellationToken = default);
        Task AddAsync(Notification notification, CancellationToken cancellationToken = default);
        void Delete(Notification notification);
    }
}
